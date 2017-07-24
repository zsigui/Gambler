using Gambler.Config;
using System;
using System.Collections.Generic;

namespace Gambler.Bet.Task
{
    public abstract class IBetTask: ITask
    {
        protected BetMatchInfo _info;

        public IBetTask(BetMatchInfo info) : base()
        {
            _info = info;
        }

        /// <summary>
        /// 共享依赖任务的超时时间
        /// </summary>
        /// <param name="dependTask">前置依赖任务</param>
        public IBetTask(ITask dependTask, BetMatchInfo info) : base(dependTask)
        {
            _info = info;
        }


        public bool CanBetByBehavior(string home, string away)
        {
            Dictionary<string, string> dic = BManager.Instance.betCountDict;
            string matchCount;
            if (!dic.TryGetValue(_info.mid, out matchCount))
            {
                string s = String.Format("{0};{1};{2}", _info.mid,
                    _info.isHomePen ? _info.home : _info.away, CurrentTime() / 1000);
                dic.Add(_info.mid, s);
            }
            GlobalSetting gs = GlobalSetting.GetInstance();
            if (matchCount != null)
            {
                String[] s = matchCount.Split(';');
                switch (gs.BetBehavior)
                {
                    case BetBehavior.FIRST_PEN:
                        if ((CurrentTime() - Convert.ToInt64(s[2]) < 30))
                            // 通常不太可能30s内两次点球，所以即使已存在数据，但在30s内，可能是另外任务的
                            return false;
                        break;
                    case BetBehavior.FIRST_TEAM:
                        if (((s[0].Contains(home) && _info.isHomePen)
                            || (s[0].Contains(away) && !_info.isHomePen)))
                            // 此时可以下注
                            return false;
                        break;
                }
            }
            return true;
        }
    }
}
