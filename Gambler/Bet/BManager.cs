using Gambler.Bet.Task;
using Gambler.Model.XPJ;
using Gambler.Module.X469.Model;
using Gambler.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gambler.Bet
{
    public class BManager
    {

        private volatile static BManager sInstance;
        private static object syncRoot = new object();
        private static object syncThread = new object();

        public static BManager Instance
        {
            get
            {
                if (sInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (sInstance == null)
                            sInstance = new BManager();
                    }
                }
                return sInstance;
            }
        }

        private BManager() { }

        private const int MAX_THREAD_COUNT = 1;
        private ConcurrentQueue<ITask> _taskQueue = new ConcurrentQueue<ITask>();

        /*-------------------DATA START------------------------*/
        private const long DATA_UPDATE_DIFF = 90000;
        private List<OddItem> _x159Data;
        private long _x159DataRecordTime = 0;
        private List<X469OddItem> _x469Data;
        private long _x469DataRecordTime = 0;
        /*--------------------DATA END-------------------------*/

        private volatile bool isRunning = false;
        private int workThreadCount = 0;


        public void Add(ITask task)
        {
            LogUtil.Write("添加执行任务，类型: " + task.GetType());
            task.AddCount();
            _taskQueue.Enqueue(task);
        }

        public void Start(ITask task)
        {
            Add(task);
            isRunning = true;
            ForceStartTask();
        }

        private void Stop()
        {
            isRunning = false;
        }
        
        private void ForceStartTask()
        {
            LogUtil.Write("强制执行任务，当前任务数: " + _taskQueue.Count + ", 当前线程数：" + workThreadCount + ", isRunning = " + isRunning);
            if (isRunning && !_taskQueue.IsEmpty && workThreadCount < MAX_THREAD_COUNT)
            {
                LogUtil.Write("准备开启新线程执行任务!");
                ThreadUtil.RunOnThread(new ThreadStart(() =>
                {
                    
                    ITask task;
                    while (isRunning && _taskQueue.TryDequeue(out task))
                    {
                        LogUtil.Write("剩余任务数: " + _taskQueue.Count + "\n当前执行任务: " + task.GetType()
                            + ", 任务是否超时:" + task.IsOvertime());
                        HandleTask(task);
                        ForceStartTask();
                    }

                    lock (syncThread)
                    {
                        workThreadCount--;
                        LogUtil.Write("任务线程数减少，当前线程数：" + workThreadCount);
                        if (workThreadCount == 0) isRunning = false;
                    }
                }));
                lock (syncThread)
                {
                    workThreadCount++;
                }
            }
        }


        private void HandleTask(ITask task)
        {
            if (task.IsOvertime())
                return;

            long cT = CurrentTime();
            switch (task.GetType())
            {
                case TaskType.X469_VALID_DATA:
                    if (_x469Data == null || _x469Data.Count == 0 
                        || (cT - _x469DataRecordTime > DATA_UPDATE_DIFF))
                    {
                        LogUtil.Write("执行获取X469Data任务");
                        task.Work();
                        LogUtil.Write("执行获取X469Data任务,获取数据结果：" + (task.GetData() == null ? "null": "success"));
                        if (task.GetData() != null)
                        {
                            _x469Data = task.GetData() as List<X469OddItem>;
                            _x469DataRecordTime = cT;
                        }
                        else
                        {
                            Add(task);
                        }
                    } else
                    {
                        LogUtil.Write("已经有X469Data了");
                    }

                    break;

                case TaskType.X469_BET:
                    // 判断预备数据是否为空，为空，则添加验证数据任务，并重新将本任务移入
                    if ((_x469Data == null || _x469Data.Count == 0
                        || (cT - _x469DataRecordTime > DATA_UPDATE_DIFF))
                        && !task.IsOverCount())
                    {
                        LogUtil.Write("执行获取自动Bet任务，先添加获取数据任务");
                        Add(new X469ValidDataTask(task));
                        Start(task);
                    }
                    else
                    {
                        LogUtil.Write("执行获取自动Bet任务主体");
                        task.Work(_x469Data);
                    }
                    break;
                case TaskType.X159_BET:
                    if (_x159Data == null || _x159Data.Count == 0
                        || (cT - _x159DataRecordTime > DATA_UPDATE_DIFF))
                    {
                        task.Work();
                        if (task.GetData() != null)
                        {
                            _x159Data = task.GetData() as List<OddItem>;
                            _x159DataRecordTime = cT;
                        }
                        else
                        {
                            Add(task);
                        }
                    }
                    break;
                case TaskType.X159_VALID_DATA:
                    if ((_x159Data == null || _x159Data.Count == 0
                        || (cT - _x159DataRecordTime > DATA_UPDATE_DIFF))
                        && !task.IsOverCount())
                    {
                        Add(new X159ValidDataTask(task));
                        Start(task);
                    }
                    else
                    {
                        task.Work(_x159Data);
                    }
                    break;
            }
        }

        private long CurrentTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(DateTime.Now - startTime).TotalMilliseconds;
        }

    }
}
