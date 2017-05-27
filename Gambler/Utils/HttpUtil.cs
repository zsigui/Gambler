using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Gambler.Utils
{
    public class HttpUtil
    {
        private static readonly string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
        private static readonly int NET_CONNECT_TIMEOUT = 10000;
	    private static readonly int NET_READ_TIMEOUT = 30000;
	    public static readonly string CHARSET = "UTF-8";
	
        private static string ConstructUrl(string requestUrl, Dictionary<string, string> queryDict)
        {
            if (queryDict == null || queryDict.Count == 0)
            {
                return requestUrl;
            }
            
            StringBuilder builder = new StringBuilder(requestUrl);
            int index = requestUrl.IndexOf("?");
            if (index == -1)
            {
                builder.Append('?');
            }
            else if (index != requestUrl.Length - 1)
            {
                builder.Append('&');
            }
            foreach (KeyValuePair<string, string> entry in queryDict)
            {
                builder.Append(entry.Key).Append("=")
                    .Append(HttpUtility.UrlEncode(entry.Value, Encoding.UTF8))
                    .Append('&');
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private static string ConcatBodyData(Dictionary<string, string> bodyDict)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> entry in bodyDict)
            {
                builder.Append(entry.Key).Append('=')
                    .Append(HttpUtility.UrlEncode(entry.Value, Encoding.UTF8)).Append('&');
            }
            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        private static void WriteOutputData(ref Dictionary<string, string> bodyDict, string jsonBody, HttpWebRequest request)
        {
            byte[] data = null;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            if (bodyDict != null && bodyDict.Count > 0)
            {
                data = Encoding.UTF8.GetBytes(ConcatBodyData(bodyDict));
            }
            else if (jsonBody != null && jsonBody != "")
            {
                data = Encoding.UTF8.GetBytes(jsonBody);
                request.ContentType = "application/json; charset=UTF-8";
            }
            if (data != null)
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                request.ContentLength = 0;
            }
        }

        public static HttpWebResponse RequestForResponse(string requestUrl, Method method, WebHeaderCollection headers, CookieCollection cookies, WebProxy proxy,
            Dictionary<string, string> queryDict, Dictionary<string, string> bodyDict, string jsonBody)
        {
            if (requestUrl == null || requestUrl == "")
                throw new ArgumentNullException("requestUrl");
            
            HttpWebRequest request = null;
            string realUrl = ConstructUrl(requestUrl, queryDict);
            if (requestUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                // Https 需要对服务端证书进行有效性校验
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(DefaultHttpsValidationCallback);
                request = WebRequest.Create(realUrl) as HttpWebRequest;
                // 避免 Https 错误
                request.ProtocolVersion = HttpVersion.Version10;
            } else
            {
                request = WebRequest.Create(realUrl) as HttpWebRequest;
            }

            // 添加Cookie信息，与Header分开处理
            proxy = new WebProxy("127.0.0.1", 8888);
            if (proxy != null)
            {
                request.Proxy = proxy;
            }

            if (headers != null)
            {
                request.Headers = headers;
            }
            else if (request.Headers == null)
            {
                request.Headers = new WebHeaderCollection();
            }


            // 初始化Cookie防止返回接收不到Response.SetCookie
            if (request.CookieContainer == null)
            {
                request.CookieContainer = new CookieContainer();
            }
            if (cookies != null)
            {
                request.CookieContainer.Add(cookies);
            }
            request.Accept = "text/plain, */*; q=0.01";


            request.Method = method.ToString();
            request.ReadWriteTimeout = NET_READ_TIMEOUT;
            request.Timeout = NET_CONNECT_TIMEOUT;

            // 默认不进行自动重定向
            request.AllowAutoRedirect = false;

            // 设置默认的 UserAgent
            String ua = request.Headers.Get("User-Agent");
            if (String.IsNullOrEmpty(ua))
            {
                request.UserAgent = DEFAULT_USER_AGENT;
            }

            // 将数据写出到远端服务器
            WriteOutputData(ref bodyDict, jsonBody, request);

            return request.GetResponse() as HttpWebResponse;
        }

        public static void RequestSync<P>(string requestUrl, Method method, WebHeaderCollection headers, CookieCollection cookies, WebProxy proxy,
            Dictionary<string, string> queryDict, Dictionary<string, string> bodyDict, string jsonBody, ConvertDataHandler<P> converData,
            OnFinishHandler<P> onFinish, OnErrorHandler onError)
        {

            if (String.IsNullOrEmpty(requestUrl))
                throw new ArgumentNullException("requestUrl");
            try
            {
                using (var response = RequestForResponse(requestUrl, method, headers, cookies, proxy,
                    queryDict, bodyDict, jsonBody))
                {

                    if (converData != null && onFinish != null)
                    {
                        int statusCode = (int)response.StatusCode;
                        P data;
                        CookieCollection cookie = null;
                        if (IsCodeSucc(statusCode))
                        {
                            Stream stream = response.GetResponseStream();
                            if (response.ContentEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                stream = new GZipStream(stream, CompressionMode.Decompress);
                            }
                            data = converData.Invoke(stream);
                            cookie = response.Cookies;
                        }
                        else
                        {
                            data = default(P);
                        }

                        onFinish.Invoke(statusCode, data, cookie);
                    }
                }
            }
            catch (Exception e)
            {
                if (onError != null)
                {
                    onError.Invoke(e);
                }
                LogUtil.Write(e);
            }
        }

        public static void RequestAsync<P>(string requestUrl, Method method, WebHeaderCollection headers, CookieCollection cookies, WebProxy proxy,
            Dictionary<string, string> queryDict, Dictionary<string, string> bodyDict, string jsonBody, ConvertDataHandler<P> converData,
            OnFinishHandler<P> onFinish, OnErrorHandler onError)
        {
            if (String.IsNullOrEmpty(requestUrl))
                throw new ArgumentNullException("requestUrl");

            ThreadUtil.RunOnThread(delegate ()
            {
                RequestSync(requestUrl, method, headers, cookies, proxy,
                    queryDict, bodyDict, jsonBody, converData, onFinish, onError);
            });
        }

        public static void Post<P>(string requestUrl, WebHeaderCollection headers, CookieCollection cookies,
            Dictionary<string, string> bodyDict, ConvertDataHandler<P> converData,
            OnFinishHandler<P> onFinish, OnErrorHandler onError)
        {
            RequestSync(requestUrl, Method.POST, headers, cookies, null, null, bodyDict, null, converData, onFinish, onError);
        }

        public static void Post<P>(string requestUrl, WebHeaderCollection headers, CookieCollection cookies, Dictionary<string, string> queryDict,
            Dictionary<string, string> bodyDict, ConvertDataHandler<P> converData,
            OnFinishHandler<P> onFinish, OnErrorHandler onError)
        {
            RequestSync(requestUrl, Method.POST, headers, cookies, null, queryDict, bodyDict, null, converData, onFinish, onError);
        }

        public static void Get<P>(string requestUrl, WebHeaderCollection headers, CookieCollection cookies,
            Dictionary<string, string> queryDict, ConvertDataHandler<P> converData,
            OnFinishHandler<P> onFinish, OnErrorHandler onError)
        {
            RequestSync(requestUrl, Method.GET, headers, cookies, null, queryDict, null, null, converData, onFinish, onError);
        }

        public static bool IsCodeSucc(int code)
        {
            return code >= 200 && code < 400;
        }
        

        private static bool DefaultHttpsValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //             if (errors == SslPolicyErrors.None)
            //                 return true;
            //             return false;
            return true;
        }
        
        public delegate P ConvertDataHandler<P>(Stream stream);
        public delegate void OnFinishHandler<P>(int statusCode, P data, CookieCollection cookie);
        public delegate void OnErrorHandler(Exception e);

        public enum Method
        {
            GET, POST, PUT, DELETE
        }
    }
}
