using Gambler.Module.HF.Model;
using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Gambler.Module.HF
{
    public class HFClient : BaseClient
    {
        private string _account;
        private string _password;

        private HFUser _user;
        private CookieCollection _cookies;
        private WebHeaderCollection _headers;

        private HFVerifyCode _verifyCode = new HFVerifyCode(Application.StartupPath + "\\Resources\\HF_trainData");

        private CookieCollection _cookiesForH8;

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


        private Boolean IsSuccess<T>(int statusCode, HFRespBase<T> data)
        {
            return HttpUtil.IsCodeSucc(statusCode) && data != null && HFErrorMsg.IsSuccess(data.code);
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
                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
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

                           // 重新进行验证码请求
                       }
                       else
                       {
                           _cookies.Add(cookies);
                           LoginByCode(vc, retryCount, onSuccess, onFail, onError);
                       }
                       return;
                   }

                   RespOnFail<HFUser>(onFail, statusCode, null);
               },
               (e) =>
               {
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
            HttpUtil.Get(HFConfig.URL_LOGIN, _headers, _cookies, queryDict,
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
                    RespOnFail<string>(onFail, statusCode, null);
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
                // 英文为默认，50ikpw55hofypvu1m44ssh55 
                _cookiesForH8.Add(new Cookie("ASP.NET_SessionId", "ycpvbs55lxxo1i45hwslm1qu", "/", "sp4ywqb1.mywinday.com"));
            }
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
                        RespOnSuccess(onSuccess, data);
                        return;
                    }
                    RespOnFail<string>(onFail, statusCode, null);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        public  void GetOddData(OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            // 实际不需要Cookie就可以直接请求= =
            InitDefaultH8Cookie();
            // 各个参数含义还有待理解
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "ov", "1",
                "ot", "r",
                "tf", "-1",
                "TFStatus", "0",
                "update", "false",
                "mt", "0",
                "r", "96410617",
                "t", "" + TimeUtil.CurrentTimeMillis());
            HttpUtil.Post(HFConfig.URL_ODD_DATA, _headers, _cookies, queryDict,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {
                    // 执行成功会重定向操作
                    if (HttpUtil.IsCodeSucc(statusCode) && !String.IsNullOrEmpty(data))
                    {
                        // 返回的为 XML文件，需要进行解析
                        ParseOddDataXml(data);
                        RespOnSuccess(onSuccess, data);
                        return;
                    }
                    RespOnFail<string>(onFail, statusCode, null);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        private void ParseOddDataXml(string data)
        {
        }
    }
}
