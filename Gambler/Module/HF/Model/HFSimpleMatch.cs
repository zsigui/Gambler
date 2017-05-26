using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class HFSimpleMatch
    {
        /// <summary>
        /// 联赛名称
        /// </summary>
        public string League { get; set; }
        /// <summary>
        /// 主队名称
        /// </summary>
        public string Home { get; set; }
        /// <summary>
        /// 客队名称
        /// </summary>
        public string Away { get; set; }
        /// <summary>
        /// 赛事ID
        /// </summary>
        public string MID { get; set; }
        /// <summary>
        /// 比赛时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 主客队比分
        /// </summary>
        public string Score { get; set; }
    }
}
