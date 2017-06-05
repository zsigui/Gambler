using Gambler.Module.HF.Model;
using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Gambler.Module.HF
{
    public class HFClient : BaseClient
    {
        private string _account;
        private string _password;

        private HFUser _user;
        private CookieCollection _cookies;
        private WebHeaderCollection _headers;
        // 进行验证码验证
        private HFVerifyCode _verifyCode = new HFVerifyCode(Application.StartupPath + "\\Resources\\HF_trainData");

        // 存放体育H8频道的请求Cookie
        private CookieCollection _cookiesForH8;
        private bool _isH8Login;

        private Dictionary<string, HFSimpleMatch> _liveMatchs;
        private Dictionary<string, int> _liveNewsetEventIds;

        
        public Dictionary<string, HFSimpleMatch> LiveMatchs
        {
            get
            {
                return _liveMatchs;
            }

            set
            {
                _liveMatchs = value;
            }
        }

        public bool IsH8Login
        {
            get; set;
        }

        public HFClient(string account, string password)
        {
            _account = account;
            _password = password;
            InitStoredHeader();
        }

        private void InitStoredHeader()
        {
            _headers = new WebHeaderCollection();
            _headers.Add("X-Requested-Width", "XMLHttpRequest");
            _headers.Add("Accept-Encoding", "gzip, defalte");
            _headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
            SetCookie();
        }

        private void SetCookie()
        {
            if (_cookies == null)
            {
                _cookies = new CookieCollection();
            }
            _cookies.Add(new Cookie("tt", "" + new Random().NextDouble(), "/", HFConfig.URL_DOMAIN));
            _cookies.Add(new Cookie("vcode", "1", "/", HFConfig.URL_DOMAIN));
        }

        protected void RespOnFail<T>(OnFailedHandler callback, int httpStatus, HFRespBase<T> data)
        {
            if (callback != null)
            {
                if (!HttpUtil.IsCodeSucc(httpStatus))
                {
                    callback.Invoke(httpStatus, HFErrorMsg.I_C_BAD_HTTP_REQUEST, HFErrorMsg.C_BAD_HTTP_REQUEST);
                }
                else if (data != null && !HFErrorMsg.IsSuccess(data.code))
                {
                    callback.Invoke(httpStatus, data.code, HFErrorMsg.GetMessageByCode(data.code));
                }
                else
                {
                    callback.Invoke(httpStatus, HFErrorMsg.I_C_BAD_RESP_DATA, HFErrorMsg.C_BAD_RESP_DATA);
                }
            }
        }

        protected void RespOnFail(OnFailedHandler callback, int httpStatus)
        {
            if (callback != null)
            {
                if (!HttpUtil.IsCodeSucc(httpStatus))
                {
                    callback.Invoke(httpStatus, HFErrorMsg.I_C_BAD_HTTP_REQUEST, HFErrorMsg.C_BAD_HTTP_REQUEST);
                }
                else
                {
                    callback.Invoke(httpStatus, HFErrorMsg.I_C_BAD_RESP_DATA, HFErrorMsg.C_BAD_RESP_DATA);
                }
            }
        }


        private Boolean IsSuccess<T>(int statusCode, HFRespBase<T> data)
        {
            return HttpUtil.IsCodeSucc(statusCode) && data != null && HFErrorMsg.IsSuccess(data.code);
        }

        private bool JudgeAndDoRetryLogin(int retryCount)
        {
            if (retryCount == 0)
                return false;
            return true;
        }

        public void Login(OnSuccessHandler<HFRespBase<HFUser>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Login(4, onSuccess, onFail, onError);
        }

        public void Login(int retryCount, OnSuccessHandler<HFRespBase<HFUser>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {

            Dictionary<string, string> queryDict = ConstructKeyValDict("s", "" + new Random().NextDouble());

            HttpUtil.Get<byte[]>(HFConfig.URL_VERICODE, _headers, _cookies, queryDict,
               (data) =>
               {
                   return IOUtil.Read(data);
               },
               (statusCode, data, cookies) =>
               {
                   if (statusCode == 200 && data != null)
                   {
                       string vc = _verifyCode.ParseCode(data);
                       Console.WriteLine("验证码: " + vc);
                       if (String.IsNullOrEmpty(vc) || vc.Length != 4)
                       {
                           if (retryCount == 0)
                           {
                               if (onFail != null)
                                   onFail.Invoke(statusCode, HFErrorMsg.I_C_FAIL_TO_VERIFY_CODE, HFErrorMsg.C_FAIL_TO_VERIFY_CODE);
                               return;
                           }

                           Login(retryCount - 1, onSuccess, onFail, onError);
                       }
                       else
                       {
                           _cookies.Add(cookies);
                           LoginByCode(vc, retryCount, onSuccess, onFail, onError);
                       }
                       return;
                   }
                   if (retryCount != 0)
                   {
                       Login(retryCount - 1, onSuccess, onFail, onError);
                       return;
                   }
                   RespOnFail<HFUser>(onFail, statusCode, null);
               },
               (e) =>
               {
                   if (retryCount != 0)
                   {
                       Login(retryCount - 1, onSuccess, onFail, onError);
                       return;
                   }
                   RespOnError(onError, e);
               });
        }

        public void LoginByCode(string code, int retryCount,
            OnSuccessHandler<HFRespBase<HFUser>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                 "username", _account,
                 "password", _password,
                 "varcode", code);
            HttpUtil.Post(HFConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<HFRespBase<HFUser>>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (IsSuccess(statusCode, data))
                   {
                       string userCookie = JsonUtil.toJson(data.data);
                       cookies.Add(new Cookie("usercookie", System.Web.HttpUtility.UrlEncode(userCookie, Encoding.UTF8)));
                       _user = data.data;
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   if (retryCount != 0)
                   {
                       Login(retryCount - 1, onSuccess, onFail, onError);
                       return;
                   }
                   Logout(null, null, null);
                   RespOnFail(onFail, statusCode, data);
               },
               (e) =>
               {
                   if (retryCount != 0)
                   {
                       Login(retryCount - 1, onSuccess, onFail, onError);
                       return;
                   }
                   Logout(null, null, null);
                   RespOnError(onError, e);
               });
        }


        public void Logout(OnSuccessHandler<HFRespBase<int>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            if (_user == null)
            {
                RespOnSuccess(onSuccess, null);
                return;
            }
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                 "username", _user.username,
                 "oid", _user.oid);
            HttpUtil.Post(HFConfig.URL_LOGOUT, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<HFRespBase<int>>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (IsSuccess(statusCode, data))
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }
                   RespOnFail(onFail, statusCode, data);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });

            // 成功与否都当成退出游戏
            _user = null;
        }

        // 以下获取H8相关页面内容（包括Live）

        public void LoginForH8(OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            if (_user == null)
            {
                if (onFail != null)
                {
                    onFail.Invoke(200, HFErrorMsg.I_LOGOUT, HFErrorMsg.GetMessageByCode(HFErrorMsg.I_LOGOUT));
                }
                return;
            }
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                 "action", "h8",
                 "username", _user.username,
                 "oid", _user.oid,
                 "lottoType", "PC",
                 "r", "" + new Random().NextDouble());
            HttpUtil.Get(HFConfig.URL_LOGIN_H8, _headers, _cookies, queryDict,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {

                    if (HttpUtil.IsCodeSucc(statusCode) && !String.IsNullOrEmpty(data))
                    {
                        Console.WriteLine("获取H8链接是: " + data);
                        LoginForH8Valid(data, onSuccess, onFail, onError);
                        return;
                    }
                    RespOnFail(onFail, statusCode);
                }, 
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        private void InitDefaultH8Cookie()
        {
            if (_cookiesForH8 == null)
            {
                _cookiesForH8 = new CookieCollection();
                //_cookiesForH8.Add(new Cookie("ASP.NET_SessionId", "ycpvbs55lxxo1i45hwslm1qu", "/", "sp4ywqb1.mywinday.com"));
            }
        }

        /// <summary>
        /// 直接添加H8 Cookie，然后就可以不用通过之前登录步骤的请求了
        /// </summary>
        /// <param name="sesseionId"></param>
        /// <param name="auth"></param>
        public void AddH8Cookie(string sesseionId, string auth)
        {
            InitDefaultH8Cookie();
            _cookiesForH8.Add(new Cookie("ASP.NET_SessionId", sesseionId, "/", "sp4ywqb1.mywinday.com"));
            _cookiesForH8.Add(new Cookie(".ASPXAUTH", auth, "/", "sp4ywqb1.mywinday.com"));
        }

        private void LoginForH8Valid(string url, OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            HttpUtil.Get(url, _headers, _cookiesForH8, null,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {
                    Console.WriteLine("H8验证结果: " + data);
                    // 执行成功会重定向操作
                    if (statusCode == 302 && data != null && data.Contains("http://sp4ywqb1.mywinday.com/main.aspx"))
                    {
                        InitDefaultH8Cookie();
                        _cookiesForH8.Add(cookies);
                        IsH8Login = true;
                        RespOnSuccess(onSuccess, data);
                        return;
                    }
                    RespOnFail(onFail, statusCode);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        /**
         * 获取今日直播比赛的ID列表
         */
        public void GetOddData(OnSuccessHandler<List<HFSimpleMatch>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            // 实际不需要Cookie就可以直接请求= =
            InitDefaultH8Cookie();
            // 各个参数含义还有待理解
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "ov", "1",
                "ot", "r",
                "mt", "0",
                "t", "" + TimeUtil.CurrentTimeMillis(),
                "tf", "-1",
                "TFStatus", "0",
                "update", "false",
                "r", "1889011725");
            HttpUtil.Post(HFConfig.URL_ODD_DATA_BS, _headers, _cookiesForH8, queryDict, null,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {
                    if (HttpUtil.IsCodeSucc(statusCode) && !String.IsNullOrEmpty(data))
                    {
                        List<HFSimpleMatch> matchList = HFHtmlParser.ParseOddDataXml(data);
                        ResetLiveMatch(matchList);
                        RespOnSuccess(onSuccess, matchList);
                        return;
                    }
                    RespOnFail(onFail, statusCode);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        private void ResetLiveMatch(List<HFSimpleMatch> matchList)
        {
            if (LiveMatchs == null)
                LiveMatchs = new Dictionary<string, HFSimpleMatch>();

            LiveMatchs.Clear();

            foreach (HFSimpleMatch m in matchList)
            {
                if (!String.IsNullOrEmpty(m.MID) && !LiveMatchs.ContainsKey(m.MID))
                {
                    LiveMatchs.Add(m.MID, m);
                }
            }
        }

        private void AddMatchEventId(string matchId, int newsetEId)
        {
            if (LiveMatchs == null)
                return;
            if (_liveNewsetEventIds == null)
                _liveNewsetEventIds = new Dictionary<string, int>();

            if (LiveMatchs.ContainsKey(matchId))
            {
                _liveNewsetEventIds.Remove(matchId);
                _liveNewsetEventIds.Add(matchId, newsetEId);
            }
        }

        public void GetAllLiveEvent(string matchId, OnSuccessHandler<List<HFLiveEvent>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "matchId", matchId,
                "_", TimeUtil.CurrentTimeMillis() + "");
            HttpUtil.Get(HFConfig.URL_REAL_TIME, null, null, queryDict,
                (data) =>
                {
                    return JsonUtil.fromJson<List<HFLiveEvent>>(IOUtil.ReadString(data));
                },
                (statusCode, data, cookies) =>
                {
                    if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.Count > 0)
                    {
                        AddMatchEventId(matchId, data[data.Count - 1].EID);
                        RespOnSuccess(onSuccess, data);
                        return;
                    }
                    RespOnFail(onFail, statusCode);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        public void GetSpecLiveEvent(string matchId, int startEventId,
            OnSuccessHandler<HFLiveEvent> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "matchId", matchId,
                "startEventId", startEventId + "",
                "endEventId", (startEventId + 20) + "",
                "_", TimeUtil.CurrentTimeMillis() + "");
            HttpUtil.Get(HFConfig.URL_REAL_TIME, null, null, queryDict,
                (data) =>
                {
                    return JsonUtil.fromJson<List<HFLiveEvent>>(IOUtil.ReadString(data));
                },
                (statusCode, data, cookies) =>
                {
                    if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.Count > 0)
                    {
                        HFLiveEvent ev = data[0];
                        AddMatchEventId(matchId, ev.EID);
                        RespOnSuccess(onSuccess, ev);
                        return;
                    }
                    RespOnFail(onFail, statusCode);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        public void GetSpecLiveEvent(string matchId, OnSuccessHandler<HFLiveEvent> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            int startId;
            if (_liveNewsetEventIds != null && _liveNewsetEventIds.TryGetValue(matchId, out startId))
            {
                GetSpecLiveEvent(matchId, startId, onSuccess, onFail, onError);
            }
            else
            {

                Dictionary<string, string> queryDict = ConstructKeyValDict(
                    "matchId", matchId,
                    "_", TimeUtil.CurrentTimeMillis() + "");
                HttpUtil.Get(HFConfig.URL_REAL_TIME, null, null, queryDict,
                    (data) =>
                    {
                        return JsonUtil.fromJson<List<HFLiveEvent>>(IOUtil.ReadString(data));
                    },
                    (statusCode, data, cookies) =>
                    {
                        if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.Count > 0)
                        {
                            HFLiveEvent ev = data[data.Count - 1];
                            AddMatchEventId(matchId, ev.EID);
                            GetSpecLiveEvent(matchId, ev.EID, onSuccess, onFail, onError);
                            return;
                        }
                        RespOnFail(onFail, statusCode);
                    },
                    (e) =>
                    {
                        RespOnError(onError, e);
                    });
            }
        }
    }
}
