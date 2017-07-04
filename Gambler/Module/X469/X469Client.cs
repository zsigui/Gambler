using Gambler.Module.X469.Model;
using Gambler.Utils;
using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Gambler.Module.X469
{
    public class X469Client : BaseClient
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

        public X469Client(string account, string password)
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
            Login(4, onSuccess, onFail, onError);
        }

        public void Login(int retryCount, OnSuccessHandler<X469Login> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {

            Dictionary<string, string> queryDict = ConstructKeyValDict("_r", "" + new Random().NextDouble());

            HttpUtil.Get<byte[]>(X469Config.URL_VERICODE, _headers, _cookies, _proxy, queryDict,
               (data) =>
               {
                   return IOUtil.Read(data);
               },
               (statusCode, data, cookies) =>
               {
                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       _cookies.Add(cookies);
                       string vc = _verifyCode.ParseCode(data);
                       Console.WriteLine("验证码: " + vc);
                       if (String.IsNullOrEmpty(vc) || vc.Length != 4)
                       {
                           if (retryCount == 0)
                           {
                               if (onFail != null)
                                   onFail.Invoke(statusCode, BaseError.I_C_FAIL_TO_VERIFY_CODE, BaseError.C_FAIL_TO_VERIFY_CODE);
                               return;
                           }

                           Login(retryCount - 1, onSuccess, onFail, onError);

                           // 重新进行验证码请求
                       }
                       else
                       {
                           LoginByCode(vc, retryCount, onSuccess, onFail, onError);
                       }
                       return;
                   }

                   RespOnFail(onFail, statusCode, BaseError.I_C_FAIL_TO_VERIFY_CODE, "");
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void LoginByCode(string code, int retryCount,
            OnSuccessHandler<X469Login> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                 "username", _account,
                 "password", _password,
                 "captcha", code);
            HttpUtil.Post(X469Config.URL_LOGIN, _headers, _cookies, _proxy, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<X469Login>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       if (data.result.Equals("1") || data.result.Equals("2"))
                       {
                           if (retryCount == 0)
                           {
                               RespOnFail(onFail, statusCode, BaseError.I_C_FAIL_TO_VERIFY_CODE, BaseError.C_FAIL_TO_VERIFY_CODE);
                               return;
                           }

                           Login(retryCount - 1, onSuccess, onFail, onError);
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
            HttpUtil.Post(X469Config.URL_USER, _headers, _cookies, _proxy, null,
                (data) =>
                {
                    return JsonUtil.fromJson<X469User>(IOUtil.ReadString(data));
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
            if (String.IsNullOrEmpty(_uid))
            {
                // 先获取uid
                return;
            }
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                "action", action,
                "page", pageNo.ToString(),
                "data", "json",
                "uid", _uid,
                "keyword", leagues,
                "_", String.Format("{0}", TimeUtil.CurrentTimeMillis() / 1000));
            HttpUtil.Get(X469Config.URL_ODD_DATA, _headers, _cookies, _proxy, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<X469OddData>(IOUtil.ReadString(data));
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

        private void GetAllOddDataByPage(string gameType, int page, X469OddData fromLast,
            OnSuccessHandler<X469OddData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
               "gameType", gameType,
               "pageNo", page.ToString());
            HttpUtil.Post(X469Config.URL_ODD_DATA, _headers, _cookies, _proxy, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<X469OddData>(IOUtil.ReadString(data));
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
                            GetAllOddDataByPage(gameType, page + 1, fromLast,
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

        public void DoBet(OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> queryDict = ConstructKeyValDict("uid", _uid);
            Dictionary<string, string> bodyDict = ConstructKeyValDict("data", "");
            HttpUtil.Post(X469Config.URL_BET, _headers, _cookies, _proxy, bodyDict,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       if (data.Contains("false|"))
                       {
                           RespOnFail(onFail, statusCode, BaseError.I_C_BAD_HTTP_REQUEST, data.Substring(6));
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
    }
}
