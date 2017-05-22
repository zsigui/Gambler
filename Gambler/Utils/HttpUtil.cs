using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gambler.Utils
{
    public class HttpUtil
    {
        private static readonly string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
        private static readonly int NET_CONNECT_TIMEOUT = 10 * 000;
	    private static readonly int NET_READ_TIMEOUT = 30 * 000;
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
            if (bodyDict != null && bodyDict.Count > 0)
            {
                data = Encoding.ASCII.GetBytes(ConcatBodyData(bodyDict));
            }
            else if (jsonBody != null && jsonBody != "")
            {
                data = Encoding.ASCII.GetBytes(jsonBody);
            }
            if (data != null)
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        public static void Request<P>(string requestUrl, Method method, WebHeaderCollection headers, CookieCollection cookies, WebProxy proxy,
            Dictionary<string, string> queryDict, Dictionary<string, string> bodyDict, string jsonBody, IHttpCallback<P> callback)
        {
            if (requestUrl == null || requestUrl == "")
                throw new ArgumentNullException("requestUrl");

            try
            {
                HttpWebRequest request = null;
                string realUrl = ConstructUrl(requestUrl, queryDict);
                if (requestUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    // Https 需要对服务端证书进行有效性校验
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(DefaultHttpsValidationCallback);
                }
                request = WebRequest.Create(realUrl) as HttpWebRequest;
                if (proxy != null)
                {
                    request.Proxy = proxy;
                }
                if (request.Headers != null)
                {
                    request.Headers = headers;
                }

                // 添加Cookie信息，与Header分开处理
                if (cookies != null)
                {
                    request.CookieContainer.Add(cookies);
                }


                request.Method = method.ToString();
                request.ReadWriteTimeout = NET_READ_TIMEOUT;
                request.Timeout = NET_CONNECT_TIMEOUT;


                // 设置默认的 UserAgent
                String ua = request.Headers.Get("UserAgent");
                if (ua == null || ua == "")
                {
                    request.UserAgent = DEFAULT_USER_AGENT;
                }

                // 将数据写出到远端服务器
                WriteOutputData(ref bodyDict, jsonBody, request);

                // 无返回需求时不继续执行处理
                if (callback == null)
                    return;

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    P data;
                    CookieCollection cookie = null;
                    if (isCodeSucc(statusCode))
                    {
                        Stream stream = response.GetResponseStream();
                        if (response.ContentEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            stream = new GZipStream(stream, CompressionMode.Decompress);
                        }
                        data = callback.ConvertData(stream);
                        cookie = response.Cookies;
                    }
                    else
                    {
                        data = default(P);
                    }
                    callback.OnFinish(statusCode, data, cookie);
                }
            }
            catch (Exception e)
            {
                if (callback != null)
                {
                    callback.OnError(e);
                }
            }
            
        }

        private static bool isCodeSucc(int code)
        {
            return code >= 200 && code < 400;
        }
        

        private static bool DefaultHttpsValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        public interface IHttpCallback<P>
        {
            P ConvertData(Stream stream);

            void OnFinish(int statusCode, P data, CookieCollection cookie);

            void OnError(Exception e);
        }

        public enum Method
        {
            GET, POST, PUT, DELETE
        }
    }
}
