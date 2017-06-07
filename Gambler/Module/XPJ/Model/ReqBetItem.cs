using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Model.XPJ
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ReqBetItem
    {
        public int gid;

        public string odds;

        // "ior_OUH" 字符串之类的
        public string type;

        // CON_OUH 所代表的值
        public string project;

        public string scoreH;

        public string scoreC;

        // 篮球时采用
        public string mid;
    }
}
