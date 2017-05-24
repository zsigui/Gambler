using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Model.XPJ
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RespBet : RespBase
    {
        /**
	     * 失败时新的赔率值
	     */
        public float newOdds;

        /**
         * 成功时的下注单号
         */
        public string code;
    }
}
