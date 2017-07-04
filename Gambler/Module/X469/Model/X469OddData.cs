using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gambler.Module.X469.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class X469OddData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public string totalpage;

        /// <summary>
        /// 当前页数
        /// </summary>
        public string curpage;

        public List<X469OddItem> results;
    }
}
