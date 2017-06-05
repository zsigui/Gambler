using Gambler.XPJ;
using Newtonsoft.Json;
using System.Net;

namespace Gambler.Module.XPJ.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class XPJAccount
    {
        private bool _isChecked = true;

        [JsonProperty]
        public string Account { set; get; }

        [JsonProperty]
        public string Password { set; get; }

        [JsonProperty]
        public string Address { set; get; }

        [JsonProperty]
        public int Port { set; get; }

        [JsonProperty]
        public string ProxyUsername { set; get; }

        [JsonProperty]
        public string ProxyPwd { set; get; }

        [JsonProperty]
        public bool IsChecked
        {
            set { _isChecked = value; }
            get { return _isChecked; }
        }

        public float Money { set; get; }

        public WebProxy Proxy { set; get; }

        private XPJClient _client;

        /// <summary>
        /// 需要保证在设置完账号密码之后调用
        /// </summary>
        public XPJClient newClient()
        {
            _client = new XPJClient(Account, Password);
            if (Proxy != null)
            {
                _client.Proxy = Proxy;
            }
            return _client;
        }

        public XPJClient GetClient()
        {
            if (_client == null)
                newClient();
            return _client;
        }
    }
}
