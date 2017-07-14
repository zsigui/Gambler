using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module
{
    public class BaseClient
    {

        protected WebProxy _proxy;
        public WebProxy Proxy
        {
            set
            {
                _proxy = value;
            }
        }
        /// <summary>
        /// 统一构造键值对字典
        /// </summary>
        /// <param name="data">键值列表：k1,v1,k2,v2,...</param>
        /// <returns></returns>
        protected Dictionary<string, string> ConstructKeyValDict(params string[] data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int len = data.Length - 1;
            for (int i = 0; i < len; i += 2)
            {
                dict.Add(data[i], data[i + 1]);
            }
            return dict;
        }

        protected void RespOnError(OnErrorHandler callback, Exception t)
        {
            LogUtil.Write(t);
            if (callback != null)
            {
                callback.Invoke(t);
            }
        }

        protected void RespOnSuccess<T>(OnSuccessHandler<T> callback, T data)
        {
            if (callback != null)
            {
                callback.Invoke(data);
            }
        }

        public delegate void OnSuccessHandler<T>(T data);

        public delegate void OnFailedHandler(int httpStatus, int errcode, String errorMsg);

        public delegate void OnErrorHandler(Exception e);

    }
}
