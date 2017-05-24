using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Model.XPJ
{
    [JsonObject(MemberSerialization.OptOut)]
    public class DataGameCount
    {
        /**
	     * 今日联赛 / 篮球
	     */
        public int TD_BK;
        /**
         * 早盘 / 篮球
         */
        public int FT_BK;
        /**
         * 滚球 / 篮球
         */
        public int RB_BK;
        /**
         * 今日联赛 / 足球
         */
        public int TD_FT;
        /**
         * 早盘 / 足球
         */
        public int FT_FT;
        /**
         * 滚球 / 足球
         */
        public int RB_FT;
    }
}
