using Gambler.Config;
using Gambler.Model;
using Gambler.Model.XPJ;
using Gambler.Module.XPJ.Model;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Bet.Task
{
    public class X159BetTask : ITask
    {

        private BetMatchInfo _info;

        public X159BetTask(BetMatchInfo info) : base() {
            if (info == null)
                throw new ArgumentNullException("info不能为空");
            _info = info;
        }

        public X159BetTask(ITask task, BetMatchInfo info) : base(task) {
            if (info == null)
                throw new ArgumentNullException("info不能为空");
            _info = info;
        }

        public override TaskType GetType()
        {
            return TaskType.X159_BET;
        }

        public override void Work(object param = null)
        {
            if(param == null || !(param is List<XPJOddData>))
                return;
            LogUtil.Write("任务执行中，类型: " + GetType());
            List<IntegratedAccount> accounts = FormMain.GetInstance().ObtainAccounts(AcccountType.XPJ469);
            if (accounts == null || accounts.Count <= 0)
                return;
            
            ReqBetData reqData = new ReqBetData();

            GlobalSetting gs = GlobalSetting.GetInstance();
            BetType firstType = gs.GameBetType, secondType = gs.SecondBetType;

            string league = gs.GetMapValue(GlobalSetting.X159_KEY, _info.league);
            string home = gs.GetMapValue(GlobalSetting.X159_KEY, _info.home);
            string away = gs.GetMapValue(GlobalSetting.X159_KEY, _info.away);

            List<XPJOddData> items = param as List<XPJOddData>;
            items = SearchByInfo(items, league, home, away);

            reqData.money = gs.BetMoney.ToString();
            reqData.acceptBestOdds = true;
            reqData.plate = "H";
            ReqBetItem reqItem = new ReqBetItem();
            reqData.items = new List<ReqBetItem>() { reqItem };

            JudgeBetInfo(reqItem, firstType, items);

            if (reqItem.gid == 0)
            {
                // 次指定类型判断
                JudgeBetInfo(reqItem, secondType, items);
            }

            if (reqItem.gid == 0)
                // 还是查找不到适合下注的，任务失败
                return;

            // 执行下注请求
            foreach (IntegratedAccount acc in accounts)
            {
                acc.GetClient<XPJClient>().DoBet(reqData,
                    (data) =>
                    {
                        LogUtil.Write("下注成功! Account = " + acc.Account + ", Type = " + acc.Type);
                    }, null, null);
            }
        }

        private List<XPJOddData> SearchByInfo(List<XPJOddData> items, string league, string home, string away)
        {
            List<XPJOddData> result = new List<XPJOddData>();
            foreach (XPJOddData item in items)
            {
                if (item.league.Contains(league) &&
                    item.home.Contains(home) && item.guest.Contains(away))
                    result.Add(item);
            }
            return result;
        }

        public void JudgeBetInfo(ReqBetItem reqData, BetType betType, List<XPJOddData> items)
        {
            if (betType == BetType.None) return;

            int lastGid = 0;
            double lastRate = 0;
            double tmpRate;
            reqData.scoreC = items[0].scoreC;
            reqData.scoreH = items[0].scoreH;
            bool isHomeConcede;
            // 优先指定类型判断
            switch (betType)
            {
                case BetType.HalfBigOrSmall:
                    reqData.type = "ior_HOUH";
                    foreach (XPJOddData item in items)
                    {
                        if ((tmpRate = BigOrSmall(item.ior_HOUH, item.ior_HOUC,
                            item.CON_HOUH, item.scoreH, item.scoreC, lastRate)) != -1)
                        {
                            lastRate = tmpRate;
                            lastGid = item.gid;
                            reqData.scoreC = item.scoreC;
                            reqData.scoreH = item.scoreH;
                            reqData.project = item.CON_HOUH;
                        }
                    }
                    break;
                case BetType.HalfConcedePoints:
                    foreach (XPJOddData item in items)
                    {
                        isHomeConcede = !item.CON_HRH.StartsWith("-");
                        if ((tmpRate = ConcedePoint(item.ior_HRH, item.ior_HRC, isHomeConcede ? item.CON_HRH : item.CON_HRH.Substring(1),
                            item.scoreH, item.scoreC, isHomeConcede,
                            _info.isHomePen, lastRate)) != -1)
                        {
                            if (isHomeConcede)
                                reqData.type = "ior_HRH";
                            else
                                reqData.type = "ior_HRC";
                            lastRate = tmpRate;
                            lastGid = item.gid;
                            reqData.scoreC = item.scoreC;
                            reqData.scoreH = item.scoreH;
                            reqData.project = item.CON_HOUH;
                        }
                    }
                    break;
                case BetType.HalfCapot:
                    break;
                case BetType.BigOrSmall:
                    reqData.type = "ior_OUH";
                    foreach (XPJOddData item in items)
                    {
                        if ((tmpRate = BigOrSmall(item.ior_OUH, item.ior_OUC,
                            item.CON_OUH, item.scoreH, item.scoreC, lastRate)) != -1)
                        {
                            lastRate = tmpRate;
                            lastGid = item.gid;
                            reqData.project = item.CON_HOUH;
                        }
                    }
                    break;
                case BetType.ConcedePoints:
                    foreach (XPJOddData item in items)
                    {
                        isHomeConcede = !item.CON_RH.StartsWith("-");
                        if ((tmpRate = ConcedePoint(item.ior_RH, item.ior_RC, isHomeConcede? item.CON_RH : item.CON_RH.Substring(1),
                            item.scoreH, item.scoreC, isHomeConcede,
                            _info.isHomePen, lastRate)) != -1)
                        {
                            if (isHomeConcede)
                                reqData.type = "ior_RH";
                            else
                                reqData.type = "ior_RC";
                            lastRate = tmpRate;
                            lastGid = item.gid;
                            reqData.scoreC = item.scoreC;
                            reqData.scoreH = item.scoreH;
                            reqData.project = item.CON_OUH;
                        }
                    }
                    break;
                case BetType.Capot:
                    break;
            }
            reqData.gid = lastGid;
            reqData.odds = lastRate.ToString();

        }

        private double BigOrSmall(float rb, float rs, string rd,
            string sh, string sa, double lastRate)
        {
            if (rb == 0 || rs == 0)
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
                if ((tmpRate = rb) > lastRate
                    && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                {
                    return tmpRate;
                }
            }
            return -1;
        }

        /// <summary>
        /// 此处让球判断采用全赢判断
        /// </summary>
        public double ConcedePoint(float rh, float ra, string rd, string sh, string sa,
           bool homeConcede, bool homePen, double lastRate)
        {
            if (rh == 0 || ra == 0)
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
            int diff = homeConcede ? scoreHome - scoreAway : scoreAway - scoreHome;

            if (homeConcede)
            {
                // 主让客
                if (homePen)
                {
                    tmpRate = rh;
                    if (diff > odds[0] && diff > odds[1] && tmpRate > lastRate
                        && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                    {
                        return tmpRate;
                    }
                }
                else
                {
                    tmpRate = ra;
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
                    tmpRate = rh;
                    if (diff < odds[0] && diff < odds[1] && tmpRate > lastRate
                        && tmpRate > GlobalSetting.GetInstance().AutoBetRate)
                    {
                        return tmpRate;
                    }
                }
                else
                {
                    tmpRate = ra;
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
