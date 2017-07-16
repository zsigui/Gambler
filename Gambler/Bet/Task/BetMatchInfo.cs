using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Bet.Task
{
    public class BetMatchInfo
    {
        /// <summary>
        /// 联赛名
        /// </summary>
        public string league;

        /// <summary>
        /// 主队
        /// </summary>
        public string home;

        /// <summary>
        /// 客队
        /// </summary>
        public string away;

        /// <summary>
        /// 是否是主队点球，否则为客队点球
        /// </summary>
        public bool isHomePen;
    }
}
