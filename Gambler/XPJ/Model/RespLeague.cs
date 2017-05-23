using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Model.XPJ
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RespLeague : RespBase
    {
        public List<string> leagues;
    }
}
