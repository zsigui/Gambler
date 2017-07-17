using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.X469.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class X469ReqBetData
    {

        public double money;

        public string bet;

        public string rate;

        public string ltype;

        public string mid;

        public bool autoOpt;
    }
}
