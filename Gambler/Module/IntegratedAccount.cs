using Gambler.Module.X469;
using Gambler.XPJ;
using Newtonsoft.Json;
using System.Net;

namespace Gambler.Module.XPJ.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class IntegratedAccount
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

        [JsonProperty]
        public int Type { set; get; }

        public double Money { set; get; }

        public WebProxy Proxy { set; get; }

        private BaseClient _client;

        /// <summary>
        /// 需要保证在设置完账号密码之后调用
        /// </summary>
        public T newClient<T>() where T : BaseClient
        {
            switch (Type)
            {
                case AcccountType.XPJ155:
                    _client = new XPJClient(Account, Password);
                    break;
                case AcccountType.XPJ469:
                    _client = new X469Client(Account, Password);
                    break;
            }
            if (Proxy != null && _client != null)
            {
                _client.Proxy = Proxy;
            }
            return (T)_client;
        }

        /// <summary>
        /// 需要保证在设置完账号密码之后调用，同时要确保获取的对象跟设置的类型一致，以保证转换正确
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetClient<T>() where T : BaseClient
        {
            if (_client == null)
                newClient<T>();
            return (T)_client;
        }
    }

    public class AcccountType
    {
        public const int XPJ155 = 0;
        public const int XPJ469 = 1;
    }
}
