using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class HFUserMoney
    {
        public string money;
    }
}
