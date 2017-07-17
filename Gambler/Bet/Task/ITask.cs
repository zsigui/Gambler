using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Bet
{
    public abstract class ITask
    {
        protected long _firstRecordTime;
        protected object _data;
        protected int count;

        public ITask()
        {
            _firstRecordTime = CurrentTime();
            count = 0;
        }

        /// <summary>
        /// 共享依赖任务的超时时间
        /// </summary>
        /// <param name="dependTask">前置依赖任务</param>
        public ITask(ITask dependTask)
        {
            _firstRecordTime = dependTask._firstRecordTime;
            count = dependTask.count;
        }

        const int OVERTIME = 10000;


        public abstract void Work(object param = null);
       
        public new abstract TaskType GetType();

        public object GetData()
        {
            return _data;
        }

        public virtual bool IsOvertime()
        {
            return (CurrentTime() - _firstRecordTime) > OVERTIME;
        }

        protected long CurrentTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }
        
        public void AddCount()
        {
            count++;
        }

        public bool IsOverCount()
        {
            return count > 1;
        }
    }

    public enum TaskType
    {
        X469_VALID_DATA,
        X159_VALID_DATA,
        X469_BET,
        X159_BET,
        YL5_BET
    }
}
