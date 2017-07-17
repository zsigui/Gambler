using Gambler.Module.X469.Model;
using Gambler.Module.YL5;
using Gambler.Utils;
using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.Module.X469
{
    public class YL5Client : BaseClient
    {
        //
        private string _account;
        private string _password;
        private WebProxy _proxy;
        private CookieCollection _cookies;
        private WebHeaderCollection _headers;
        private IVerifyCode _verifyCode = new X469VerifyCode(Application.StartupPath + "\\Resources\\XPJ_trainData");

        private string _uid;

        public WebProxy Proxy
        {
            set
            {
                _proxy = value;
            }
        }

        public YL5Client(string account, string password)
        {
            _account = account;
            _password = password;
            _cookies = new CookieCollection();

            InitStoredHeader();
        }

        private void InitStoredHeader()
        {
            _headers = new WebHeaderCollection();
            _headers.Add("X-Requested-With", "XMLHttpRequest");
            _headers.Add("Accept-Encoding", "gzip, deflate");
            _headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
            _headers.Add("DNT", "1");
        }

        protected void RespOnFail(OnFailedHandler callback, int httpStatus, int code, string msg)
        {
            if (callback != null)
            {
                if (!HttpUtil.IsCodeSucc(httpStatus))
                {
                    callback.Invoke(httpStatus, httpStatus, BaseError.C_BAD_HTTP_REQUEST);
                }
                else
                {
                    callback.Invoke(httpStatus, code, msg);
                }
            }
        }

        public void Login(OnSuccessHandler<X469Login> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            LoginByCode("6666", 4, onSuccess, onFail, onError);
        }

        public void LoginByCode(string code, int retryCount,
            OnSuccessHandler<X469Login> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                 "username", _account,
                 "passwd", _password,
                 "captcha", code);
            HttpUtil.Post(YL5Config.URL_LOGIN, _headers, _cookies, _proxy, bodyDict,
                (data) =>
                {
                    string str = IOUtil.ReadString(data);
                    return JsonUtil.fromJson<X469Login>(str.Substring(1, str.Length - 2));
                },
               (statusCode, data, cookies) =>
               {

                   Console.WriteLine("LoginByCode.Data = " + data);
                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       if (data.result.Equals("1") || data.result.Equals("2"))
                       {
                           if (retryCount == 0)
                           {
                               RespOnFail(onFail, statusCode, BaseError.I_C_FAIL_TO_VERIFY_CODE, BaseError.C_FAIL_TO_VERIFY_CODE);
                               return;
                           }

                           LoginByCode(code, retryCount - 1, onSuccess, onFail, onError);
                           return;
                       }
                       else if (data.result.Equals("3"))
                       {
                           if (cookies.Count != 0)
                           {
                               _cookies.Add(cookies);
                           }
                           RespOnSuccess(onSuccess, data);
                           return;
                       }
                       else if (data.result.Equals("0"))
                       {
                           RespOnFail(onFail, statusCode, BaseError.I_C_ACCOUNT_PWD_ERROR, BaseError.C_ACCOUNT_PWD_ERROR);
                       }
                       else
                       {
                           RespOnFail(onFail, statusCode, BaseError.I_C_BAD_RESP_DATA, BaseError.C_BAD_RESP_DATA);
                       }

                       return;
                   }

                   RespOnFail(onFail, statusCode, 0, "");
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetUserInfo(OnSuccessHandler<X469User> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            HttpUtil.Post(YL5Config.URL_USER, _headers, _cookies, _proxy, null,
                (data) =>
                {
                    string str = IOUtil.ReadString(data);
                    return JsonUtil.fromJson<X469User>(str.Substring(1, str.Length - 2));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.result.Equals("1"))
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, BaseError.I_C_NO_LOGIN, BaseError.C_NO_LOGIN);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetRBOddData(OnSuccessHandler<X469OddData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            GetOddData("re", 1, "", onSuccess, onFail, onError);
        }

        public void GetOddData(string action, int pageNo, string leagues,
            OnSuccessHandler<X469OddData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "action", action,
                "page", pageNo.ToString(),
                "data", "json",
                "uid", _uid,
                "keyword", leagues,
                "_", String.Format("{0}", TimeUtil.CurrentTimeMillis()));
            HttpUtil.Get(YL5Config.URL_ODD_DATA, _headers, _cookies, _proxy, queryDict,
                (data) =>
                {
                    string str = IOUtil.ReadString(data);
                    return JsonUtil.fromJson<X469OddData>(str.Substring(1, str.Length - 3));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       RespOnSuccess(onSuccess, data);

                       return;
                   }

                   RespOnFail(onFail, statusCode, BaseError.I_C_BAD_RESP_DATA, BaseError.C_BAD_RESP_DATA);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetAllRBOddData(OnSuccessHandler<X469OddData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            GetAllOddDataByPage("re", 1, null, onSuccess, onFail, onError);
        }

        private void GetAllOddDataByPage(string action, int page, X469OddData fromLast,
            OnSuccessHandler<X469OddData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            if (String.IsNullOrEmpty(_uid))
            {
                GetUID((data) =>
                {
                    Console.WriteLine("GetAllOddDataByPage 获取完uid后备执行,uid = " + data);
                    GetAllOddDataByPage(action, page, fromLast,
                               onSuccess, onFail, onError);
                }, null, null);
                return;
            }
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "action", action,
                "page", page.ToString(),
                "data", "json",
                "uid", _uid,
                "_", String.Format("{0}", TimeUtil.CurrentTimeMillis()));
            HttpUtil.Get(YL5Config.URL_ODD_DATA, _headers, _cookies, _proxy, queryDict,
                (data) =>
                {
                    // 此处结果会多出 "();"，所以要从1下标开始并减去3长度
                    string str = IOUtil.ReadString(data);
                    return JsonUtil.fromJson<X469OddData>(str.Substring(1, str.Length - 3));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       // 设置首页数据或者添加新页数据
                       if (fromLast == null)
                       {
                           fromLast = data;
                       }
                       else
                       {
                           fromLast.results.AddRange(data.results);
                       }

                       if (Convert.ToInt32(data.totalpage) > page)
                       {
                           // 有多页，获取下一页的数据
                           GetAllOddDataByPage(action, page + 1, fromLast,
                              onSuccess, onFail, onError);
                       }
                       else
                       {
                           RespOnSuccess(onSuccess, fromLast);
                       }
                       return;
                   }

                   if (fromLast != null)
                   {
                       RespOnSuccess(onSuccess, fromLast);
                       return;
                   }
                   RespOnFail(onFail, statusCode, BaseError.I_C_BAD_RESP_DATA, BaseError.C_BAD_RESP_DATA);
               },
               (e) =>
               {
                   if (fromLast != null)
                   {
                       RespOnSuccess(onSuccess, fromLast);
                       return;
                   }
                   RespOnError(onError, e);
               });
        }

        public void DoBet(X469ReqBetData req, OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> queryDict = ConstructKeyValDict("uid", _uid);
            Dictionary<string, string> bodyDict = ConstructKeyValDict("money", String.Format("{0:N2}", req.money),
                "bet", req.bet,
                "rate", Convert.ToString(req.rate),
                "ltype", req.ltype,
                "mid", req.mid,
                "auto", req.autoOpt ? "1" : "0"
                );
            HttpUtil.Post(YL5Config.URL_BET, _headers, _cookies, queryDict, _proxy, bodyDict,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       LogUtil.Write("YL5Client.DoBet返回结果：" + data);
                       if (data.Contains("false|"))
                       {
                           RespOnFail(onFail, statusCode, BaseError.I_C_BAD_HTTP_REQUEST, data.Substring(6));
                           return;
                       }
                       else if (data.Contains("未登录"))
                       {
                           RespOnFail(onFail, statusCode, BaseError.I_C_BAD_HTTP_REQUEST, "账户需要重新登录");
                           return;
                       }
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, BaseError.I_C_BAD_RESP_DATA, BaseError.C_BAD_RESP_DATA);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetUID(OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            HttpUtil.Get(YL5Config.URL_SPORT, _headers, _cookies, _proxy, null,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
               (statusCode, data, cookies) =>
               {
                   if (HttpUtil.IsCodeSucc(statusCode) && !String.IsNullOrEmpty(data))
                   {
                       Regex regex = new Regex("uid=([^\"]*)");
                       Match m = regex.Match(data);
                       GroupCollection gc = m.Groups;
                       if (gc.Count > 1)
                       {
                           _uid = gc[1].Value;
                           RespOnSuccess(onSuccess, _uid);
                           return;
                       }
                   }

                   RespOnFail(onFail, statusCode, BaseError.I_C_BAD_RESP_DATA, BaseError.C_BAD_RESP_DATA);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }
    }
}
