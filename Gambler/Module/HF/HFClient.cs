using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF
{
    public class HFClient
    {
        private string _account;
        private string _password;

        private CookieCollection _cookies;
        private WebHeaderCollection _headers;

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

        }
    }
}
