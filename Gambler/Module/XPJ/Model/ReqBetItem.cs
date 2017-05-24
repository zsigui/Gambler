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

        public string type;

        public string project;

        public string scoreH;

        public string scoreC;
    }
}
