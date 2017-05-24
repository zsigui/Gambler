using Gambler.Model;
using Gambler.Model.XPJ;
using Gambler.Utils;
using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.XPJ
{
    public class XPJClient
    {

        private readonly string KEY_SESSION = "SESSION";
        private readonly string KEY_JSESSION_ID = "JSESSIONID";


        private IVerifyCode _verifyCode = new XPJVerifyCode(Application.StartupPath + "\\Resources\\XPJ_trainData");
        private XPJRatioHelper _helper = new XPJRatioHelper();
        private CookieCollection _cookies;
        private WebHeaderCollection _headers;

        //
        private string _account;
        private string _password;

        private string _session;
        private string _jsessionId;

        public XPJClient(string account, string password)
        {
            _account = account;
            _password = password;

            _session = Guid.NewGuid().ToString();
            _jsessionId = Md5Util.EncryptToHex(_session);
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
            if (!String.IsNullOrEmpty(_session))
            {
                _cookies.Add(new Cookie(KEY_SESSION, _session, "/", XPJConfig.URL_DOMAIN));
            }
            if (!String.IsNullOrEmpty(_jsessionId))
            {
                _cookies.Add(new Cookie(KEY_JSESSION_ID, _session, "/", XPJConfig.URL_DOMAIN));
            }
        }

        /// <summary>
        /// 统一构造键值对字典
        /// </summary>
        /// <param name="data">键值列表：k1,v1,k2,v2,...</param>
        /// <returns></returns>
        private Dictionary<string, string> ConstructKeyValDict(params string[] data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int len = data.Length - 1;
            for (int i = 0; i < len; i += 2)
            {
                dict.Add(data[i], data[i + 1]);
            }
            return dict;
        }

        private void RespOnError(OnErrorHandler callback, Exception t)
        {
            LogUtil.Write(t);
            if (callback != null)
            {
                callback.Invoke(t);
            }
        }

        private void RespOnFail(OnFailedHandler callback, int httpStatus, RespBase data)
        {
            if (callback != null)
            {
                if (!HttpUtil.IsCodeSucc(httpStatus))
                {
                    callback.Invoke(httpStatus, XPJErrorMsg.I_C_BAD_HTTP_REQUEST, XPJErrorMsg.C_BAD_HTTP_REQUEST);
                }
                else if (data != null && !String.IsNullOrEmpty(data.msg))
                {
                    callback.Invoke(httpStatus, XPJErrorMsg.I_S_ERR_MSG_FROM_SERVER, data.msg);
                }
                else
                {
                    callback.Invoke(httpStatus, XPJErrorMsg.I_C_BAD_RESP_DATA, XPJErrorMsg.C_BAD_RESP_DATA);
                }
            }
        }

        private void RespOnSuccess<T>(OnSuccessHandler<T> callback, T data) where T : RespBase
        {
            if (callback != null)
            {
                callback.Invoke(data);
            }
        }

        public void Login(OnSuccessHandler<RespLogin> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Login(4, onSuccess, onFail, onError);
        }

        public void Login(int retryCount, OnSuccessHandler<RespLogin> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {

            Dictionary<string, string> queryDict = ConstructKeyValDict("timestamp", "" + TimeUtil.CurrentTimeMillis());

            HttpUtil.Get<byte[]>(XPJConfig.URL_VERICODE, _headers, _cookies, queryDict,
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
                                   onFail.Invoke(statusCode, XPJErrorMsg.I_C_FAIL_TO_VERIFY_CODE, XPJErrorMsg.C_FAIL_TO_VERIFY_CODE);
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

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void LoginByCode(string code, int retryCount,
            OnSuccessHandler<RespLogin> onSuccess, OnFailedHandler onFail, OnErrorHandler onError) {
            Dictionary<string, string > bodyDict = ConstructKeyValDict(
                 "account", _account,
                 "password", _password,
                 "verifyCode", code);
            HttpUtil.Post(XPJConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<RespLogin>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       if (!data.success && XPJErrorMsg.S_ERROR_VERIFY_CODE.Equals(data.msg, StringComparison.OrdinalIgnoreCase))
                       {
                           if (retryCount == 0)
                           {
                               RespOnFail(onFail, statusCode, data);
                               return;
                           }

                           Login(retryCount - 1, onSuccess, onFail, onError);
                           return;
                       }
                       _cookies.Add(cookies);
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetUserInfo(OnSuccessHandler<RespUser> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            HttpUtil.Post(XPJConfig.URL_LOGIN, _headers, _cookies, null,
                (data) =>
                {
                    return JsonUtil.fromJson<RespUser>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.success)
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetLeagueData(string gameType,
            OnSuccessHandler<RespLeague> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict("gameType", gameType);
            HttpUtil.Post(XPJConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<RespLeague>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.success)
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetOddData(string gameType,
            OnSuccessHandler<RespData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            GetOddData(gameType, 1, 1, null, onSuccess, onFail, onError);
        }

        public void GetOddData(string gameType, int pageNo, int sortType, string[] leagues,
            OnSuccessHandler<RespData> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                "gameType", gameType,
                "pageNo", pageNo.ToString(),
                "sortType", sortType.ToString());
            if (leagues != null && leagues.Length > 0)
            {
                bodyDict.Add("showLegs", JsonUtil.toJson(leagues));
            }
            HttpUtil.Post(XPJConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<RespData>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.games != null)
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void GetSpecificOdd(ReqBetData reqBetData,
            OnSuccessHandler<RespOdd> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict("data", JsonUtil.toJson(reqBetData));
            HttpUtil.Post(XPJConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<RespOdd>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.success)
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void DoBet(ReqBetData reqBetData,
            OnSuccessHandler<RespBet> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict("data", JsonUtil.toJson(reqBetData));
            HttpUtil.Post(XPJConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<RespBet>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (HttpUtil.IsCodeSucc(statusCode) && data != null && data.success)
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   RespOnFail(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public delegate void OnSuccessHandler<T>(T data) where T : RespBase;

        public delegate void OnFailedHandler(int httpStatus, int errcode, String errorMsg);

        public delegate void OnErrorHandler(Exception e);

        /**
         * 定义可以请求的GameType类型 <br />
         * 
         * gameType 类型参数值说明：由三部分组成，用 '_' 进行连接，FT_FT_MN <br />
         * 1.<br />
         * RB : 滚球
         * TD : 今日赛事
         * FT :  早盘
         * 2. <br />
         * FT : 足球
         * BK : 篮球
         * 3.<br />
         * MN : 独赢 ＆ 让球 ＆ 大小 ＆ 单 / 双
         * TI : 波胆
         * BC : 总入球
         * HF : 半场 / 全场
         * MX : 综合过关
         * CH : 冠军
         */
        public static class GameType
        {
            public static readonly string FT_RB_MN = "FT_RB_MN";
            public static readonly string FT_RB_TI = "FT_RB_TI";
            public static readonly string FT_RB_BC = "FT_RB_BC";
            public static readonly string FT_RB_HF = "FT_RB_HF";

            public static readonly string BK_RB_MN = "BK_RB_MN";

            public static readonly string FT_TD_MN = "FT_TD_MN";
            public static readonly string FT_TD_TI = "FT_TD_TI";
            public static readonly string FT_TD_BC = "FT_TD_BC";
            public static readonly string FT_TD_HF = "FT_TD_HF";
            public static readonly string FT_TD_MX = "FT_TD_MX";
            public static readonly string FT_TD_CH = "FT_TD_CH";

            public static readonly string BK_TD_MN = "BK_TD_MN";
            public static readonly string BK_TD_MX = "BK_TD_MX";

            public static readonly string FT_FT_MN = "FT_FT_MN";
            public static readonly string FT_FT_TI = "FT_FT_TI";
            public static readonly string FT_FT_BC = "FT_FT_BC";
            public static readonly string FT_FT_HF = "FT_FT_HF";
            public static readonly string FT_FT_MX = "FT_FT_MX";
            public static readonly string FT_FT_CH = "FT_FT_CH";

            public static readonly string BK_FT_MN = "BK_FT_MN";
            public static readonly string BK_FT_MX = "BK_FT_MX";
            public static readonly string BK_FT_CH = "BK_FT_CH";

        }

    }
    
}
