using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.X469.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class X469User
    {
        /// <summary>
        /// 0 未登录 1 已登录
        /// </summary>
        public string result;

        /// <summary>
        /// 金额
        /// </summary>
        public string money;

        /// <summary>
        /// 用户名
        /// </summary>
        public string name;

        /// <summary>
        /// BBIN金额
        /// </summary>
        public string bbinlivemoney;

        /// <summary>
        /// HG金额
        /// </summary>
        public string hglivemoney;
    }
}
