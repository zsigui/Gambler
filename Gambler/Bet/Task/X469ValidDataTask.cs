using Gambler.Module.X469;
using Gambler.Module.XPJ.Model;
using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Bet.Task
{
    public class X469ValidDataTask : ITask
    {
        public X469ValidDataTask() : base() { }

        public X469ValidDataTask(ITask task) : base(task) { }

        public override TaskType GetType()
        {
            return TaskType.X469_VALID_DATA;
        }

        public override bool IsOvertime()
        {
            return false;
        }

        public override void Work(object param = null)
        {
            LogUtil.Write("任务执行中，类型: " + GetType());
            List<IntegratedAccount> accounts = FormMain.GetInstance().ObtainAccounts(AcccountType.XPJ469);
            if (accounts != null && accounts.Count > 0)
            {
                accounts[0].GetClient<X469Client>().GetAllRBOddData(
                    (ret) =>
                    {
                        if (ret != null && ret.results != null && ret.results.Count > 0)
                            _data = ret.results;
                    }, null, null);
            }
            else
            {
                accounts = FormMain.GetInstance().ObtainAccounts(AcccountType.YL5789);
                if (accounts == null || accounts.Count <= 0)
                    return;
                accounts[0].GetClient<YL5Client>().GetAllRBOddData(
                    (ret) =>
                    {
                        if (ret != null && ret.results != null && ret.results.Count > 0)
                            _data = ret.results;
                    }, null, null);
            }
        }
    }
}
