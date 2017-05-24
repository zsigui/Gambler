using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF.Model
{
    /// <summary>
    /// 登录后返回的用户对象，数据全部设置为string，便于转换成json后设置在cookie中，键值 usercookie=urlencode(c);
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class HFUser
    {
        public string username;
        public string id;
        public string money_status;

        public string uvipname;
        public string realname;
        public string logintime;
        public string agent;
        public string world;
        public string deposit;
        public string corprator;
        public string mobile;
        public string upoints;
        public string loginip;
        public string oid;
        public string user_type;
        public string uvip;
        public string supers;
        public string ulevel;
    }
}
