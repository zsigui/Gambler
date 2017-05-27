using System.Net;

namespace Gambler.Module.XPJ.Model
{
    public class XPJAccount
    {
        public string Account { set; get; }

        public string Password { set; get; }

        public WebProxy Proxy { set; get; }
    }
}
