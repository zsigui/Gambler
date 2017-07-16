using Gambler.Module.X469;
using Gambler.Module.XPJ.Model;
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
        
        public override void Work(object param = null)
        {
            List<IntegratedAccount> accounts = FormMain.GetInstance().ObtainAccounts(AcccountType.XPJ469);
            if (accounts == null || accounts.Count <= 0)
                return;

            accounts[0].GetClient<X469Client>().GetAllRBOddData(
                (ret) => {
                    if (ret != null && ret.results != null && ret.results.Count > 0)
                        _data = ret.results;
                }, null, null);
        }
    }
}
