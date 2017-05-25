using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF.Model
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class HFLiveEvent
    {
        /// <summary>
        /// 比赛ID
        /// </summary>
        public int MID;

        /// <summary>
        /// 下一事件识别ID
        /// </summary>
        public int EID;

        /// <summary>
        /// 事件ID
        /// </summary>
        public string CID;
        
        /// <summary>
        /// 事件说明
        /// </summary>
        public string SID;
        public string Info;
        public string T;
    }
}
