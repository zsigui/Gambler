using Gambler.Bet.Task;
using Gambler.Config;
using Gambler.Module.X469;
using Gambler.Module.X469.Model;
using Gambler.Module.XPJ.Model;
using Gambler.UI;
using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Bet
{
    public class X469BetTask : ITask
    {
        private BetMatchInfo _info;

        public X469BetTask(BetMatchInfo info) : base() {
            if (info == null)
                throw new ArgumentNullException("info不能为空");
            _info = info;
        }

        public X469BetTask(ITask task, BetMatchInfo info) : base(task) {
            if (info == null)
                throw new ArgumentNullException("info不能为空");
            _info = info;
        }

        public override TaskType GetType()
        {
            return TaskType.X469_BET;
        }

        public override void Work(object param = null)
        {
            if (param == null || !(param is List<X469OddItem>))
                return;

            List<IntegratedAccount> accounts =  FormMain.GetInstance().ObtainAccounts(AcccountType.XPJ469);
            if (accounts == null || accounts.Count <= 0)
                return;
            
            X469ReqBetData reqData = new X469ReqBetData();

            GlobalSetting gs = GlobalSetting.GetInstance();
            BetType firstType = gs.GameBetType, secondType = gs.SecondBetType;

            string league = gs.GetMapValue(GlobalSetting.X469_KEY, _info.league);
            string home = gs.GetMapValue(GlobalSetting.X469_KEY, _info.home);
            string away = gs.GetMapValue(GlobalSetting.X469_KEY, _info.away);

            List<X469OddItem> items = param as List<X469OddItem>;
            items = SearchByInfo(items, league, home, away);

            reqData.money = gs.BetMoney;
            reqData.autoOpt = true;

            JudgeBetInfo(ref reqData, firstType, items);

            if (String.IsNullOrEmpty(reqData.mid))
            {
                // 次指定类型判断
                JudgeBetInfo(ref reqData, secondType, items);
            }

            if (String.IsNullOrEmpty(reqData.mid))
                // 还是查找不到适合下注的，任务失败
                return;
            
            // 执行下注请求
            foreach (IntegratedAccount acc in accounts)
            {
                acc.GetClient<X469Client>().DoBet(reqData,
                    (data) =>
                    {
                        long cs = (CurrentTime() - _firstRecordTime);
                        LogUtil.Write("下注成功! Account = " + acc.Account + ", Type = " + acc.Type);
                        ThreadUtil.WorkOnUI<string>(FormMain.GetInstance(),
                            new Action(() => {

                                new DialogNotify(String.Format("{0}[{1}]", acc.Account, acc.Type), 
                                    "下注成功", String.Format("耗时{0}ms", cs)).Show();
                            }), null);
                    }, 
                    (hc, ec, em)=> {
                        long cs = (CurrentTime() - _firstRecordTime);
                        LogUtil.Write("Account = " + acc.Account + ", Type = " + acc.Type + " 下注失败，原因：" + em
                            + ", 当前已过去时间: " + cs + " ms");
                        ThreadUtil.WorkOnUI<string>(FormMain.GetInstance(),
                            new Action(() => {

                                new DialogNotify(String.Format("{0}[{1}]", acc.Account, acc.Type),
                                    String.Format("下注失败，耗时{0}ms", cs), em).Show();
                            }), null);
                    }, null);
            }
        }

        private List<X469OddItem> SearchByInfo(List<X469OddItem> items, string league, string home, string away)
        {
            List<X469OddItem> result = new List<X469OddItem>();
            foreach (X469OddItem item in items)
            {
                if (item.a26.Equals(league) &&
                    item.a2.Equals(home) && item.a3.Equals(away))
                    result.Add(item);
            }
            return result;
        }

        private double BigOrSmall(string rb, string rs, string rd, string sh, string sa, double lastRate)
        {
            if (String.IsNullOrEmpty(rb) || String.IsNullOrEmpty(rs))
                return -1;

            double tmpRate;
            double[] odds = new double[2];
            if (rd.Contains("/"))
            {
                string[] s2 = rd.Split('/');
                odds[0] = Convert.ToDouble(s2[0]);
                odds[1] = Convert.ToDouble(s2[1]);
            }
            else
            {
                odds[0] = odds[1] = Convert.ToDouble(rd);
            }
            int totalScroe = Convert.ToInt32(sh) + Convert.ToInt32(sa) + 1;
            if (totalScroe > odds[0] && totalScroe > odds[1])
            {
                if ((tmpRate = Convert.ToDouble(rb)) > lastRate
                    && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                {
                    return tmpRate;
                }
            }
            return -1;
        }

        public void JudgeBetInfo(ref X469ReqBetData reqData, BetType betType, List<X469OddItem> items)
        {
            if (betType == BetType.None) return;

            string lastMid = null;
            double lastRate = 0;
            double tmpRate;
            // 优先指定类型判断
            switch (betType)
            {
                case BetType.HalfBigOrSmall:
                    reqData.bet = "C";
                    reqData.ltype = "30";
                    foreach (X469OddItem item in items)
                    {
                        if ((tmpRate = BigOrSmall(item.a14, item.a15,
                            item.a13, item.a16, item.a17, lastRate)) != -1)
                        {
                            lastRate = tmpRate;
                            lastMid = item.mid;
                        }
                    }
                    break;
                case BetType.HalfConcedePoints:
                    reqData.ltype = "19";
                    reqData.bet = _info.isHomePen ? "H" : "C";
                    foreach (X469OddItem item in items)
                    {
                        if ((tmpRate = ConcedePoint(item.a31, item.a32, item.a30,
                            item.a16, item.a17, !String.IsNullOrEmpty(item.a36),
                            _info.isHomePen, lastRate)) != -1)
                        {
                            lastRate = tmpRate;
                            lastMid = item.mid;
                        }
                    }
                    break;
                case BetType.HalfCapot:
                    break;
                case BetType.BigOrSmall:
                    reqData.bet = "C";
                    reqData.ltype = "10";
                    foreach (X469OddItem item in items)
                    {
                        if ((tmpRate = BigOrSmall(item.a34, item.a35,
                            item.a33, item.a16, item.a17, lastRate)) != -1)
                        {
                            lastRate = tmpRate;
                            lastMid = item.mid;
                        }
                    }
                    break;
                case BetType.ConcedePoints:
                    reqData.ltype = "9";
                    reqData.bet = _info.isHomePen ? "H" : "C";
                    foreach (X469OddItem item in items)
                    {
                        if ((tmpRate = ConcedePoint(item.a11, item.a12, item.a10,
                            item.a16, item.a17, !String.IsNullOrEmpty(item.a20),
                            _info.isHomePen, lastRate)) != -1)
                        {
                            lastRate = tmpRate;
                            lastMid = item.mid;
                        }
                    }
                    break;
                case BetType.Capot:
                    break;
            }
            reqData.rate = lastRate.ToString();
            reqData.mid = lastMid;

        }

        /// <summary>
        /// 此处让球判断采用全赢判断
        /// </summary>
        public double ConcedePoint(string rh, string ra, string rd, string sh, string sa,
           bool homeConcede, bool homePen, double lastRate)
        {
            if (String.IsNullOrEmpty(rh) || String.IsNullOrEmpty(ra))
                return -1;

            double tmpRate;
            double[] odds = new double[2];
            if (rd.Contains("/"))
            {
                string[] s2 = rd.Split('/');
                odds[0] = Convert.ToDouble(s2[0]);
                odds[1] = Convert.ToDouble(s2[1]);
            }
            else
            {
                odds[0] = odds[1] = Convert.ToDouble(rd);
            }
            int scoreHome = Convert.ToInt32(sh) + (homePen ? 1 : 0);
            int scoreAway = Convert.ToInt32(sa) + (homePen ? 0 : 1);
            int diff = Math.Abs(scoreAway - scoreHome);

            if (homeConcede)
            {
                // 主让客
                if (homePen)
                {
                    tmpRate = Convert.ToDouble(rh);
                    if (diff > odds[0] && diff > odds[1] && tmpRate > lastRate
                        && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                    {
                        return tmpRate;
                    }
                }
                else
                {
                    tmpRate = Convert.ToDouble(ra);
                    if (diff < odds[0] && diff < odds[1] && tmpRate > lastRate
                        && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                    {
                        return tmpRate;
                    }
                }
            }
            else
            {
                // 客让主
                if (homePen)
                {
                    tmpRate = Convert.ToDouble(rh);
                    if (diff < odds[0] && diff < odds[1] && tmpRate > lastRate
                        && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                    {
                        return tmpRate;
                    }
                }
                else
                {
                    tmpRate = Convert.ToDouble(ra);
                    if (diff > odds[0] && diff > odds[1] && tmpRate > lastRate
                        && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                    {
                        return tmpRate;
                    }
                }
            }
            return -1;
        }
    }
}
