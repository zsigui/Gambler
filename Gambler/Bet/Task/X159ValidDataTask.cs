using Gambler.Module.XPJ.Model;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Bet.Task
{
    public class X159ValidDataTask : ITask
    {
        public X159ValidDataTask() : base() { }

        public X159ValidDataTask(ITask task) : base(task) { }

        public override TaskType GetType()
        {
            return TaskType.X159_VALID_DATA;
        }

        public override void Work(object param = null)
        {
            List<IntegratedAccount> accounts = FormMain.GetInstance().ObtainAccounts(AcccountType.XPJ155);
            if (accounts == null || accounts.Count <= 0)
                return;
            accounts[0].GetClient<XPJClient>().GetAllOddData("FT_RB_MN",
                    1,
                    (ret) =>
                    {
                        if (ret != null && ret.games != null && ret.games.Count > 0)
                            _data = XPJDataParser.TransformRespDataToXPJOddDataList(ret);
                    }, null, null);
        }
    }
}
