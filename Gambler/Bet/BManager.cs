using Gambler.Bet.Task;
using Gambler.Model.XPJ;
using Gambler.Module.X469.Model;
using Gambler.Module.XPJ.Model;
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
        public Dictionary<string, string> betCountDict = new Dictionary<string, string>();

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

        private const int MAX_THREAD_COUNT = 2;
        private Thread[] _threads = new Thread[MAX_THREAD_COUNT];
        private ConcurrentQueue<ITask> _taskQueue = new ConcurrentQueue<ITask>();

        /*-------------------DATA START------------------------*/
        private const long DATA_UPDATE_DIFF = 90000;
        private List<OddItem> _x159Data;
        private long _x159DataRecordTime = 0;
        private List<X469OddItem> _x469Data;
        private long _x469DataRecordTime = 0;
        /*--------------------DATA END-------------------------*/

        private volatile bool isRunning = false;
        private List<TaskType> _workingType = new List<TaskType>();
        private List<TaskType> _waitType = new List<TaskType>();


        public void Add(ITask task)
        {
            //LogUtil.Write("添加执行任务，类型: " + task.GetType());
            task.AddCount();
            lock (syncRoot)
            {
                TaskType type = task.GetType();
                if (!_waitType.Contains(type) && !_workingType.Contains(type))
                {
                    if (type == TaskType.X159_VALID_DATA
                        || type == TaskType.X469_VALID_DATA)
                    {
                        LogUtil.Write("添加X469_VALID_DATA");
                        _waitType.Add(type);
                    }
                    _taskQueue.Enqueue(task);
                }
            }
        }

        public void Start(ITask task)
        {
            Add(task);
            isRunning = true;
            ForceStartTask();
        }

        public void Stop()
        {
            isRunning = false;
            for (int i = MAX_THREAD_COUNT - 1; i >= 0; i--)
            {
                if (_threads[i] != null && _threads[i].IsAlive)
                {
                    _threads[i].Interrupt();
                }
                _threads[i] = null;
            }
        }
        
        private void ForceStartTask()
        {
            for (int i = MAX_THREAD_COUNT - 1; i > 0; i--)
            {
                if (_threads[i] != null && _threads[i].IsAlive)
                    continue;
                LogUtil.Write("准备开启新线程执行任务!");
                _threads[i] = ThreadUtil.RunOnThread(new ThreadStart(() =>
                {
                    
                    ITask task;
                    while (isRunning)
                    {
                        if (_taskQueue.TryDequeue(out task))
                        {
                            HandleTask(task);
                        }
                        try
                        {
                            Thread.Sleep(20);
                        }
                        catch (Exception e)
                        {
                            LogUtil.Write(e);
                        }
                    }
                }));
            }
        }


        private void HandleTask(ITask task)
        {
            if (task.IsOvertime())
                return;
            long cT = CurrentTime();
            TaskType type = task.GetType();
            switch (task.GetType())
            {
                case TaskType.X469_BET:
                case TaskType.YL5_BET:
                    LogUtil.Write("X469_BET 或者 YL5_BET 获取不到数据，准备添加再来");
                    // 两者具有相同的请求数据结构
                    // 判断预备数据是否为空，为空，则添加验证数据任务，并重新将本任务移入
                    if (_x469Data != null && _x469Data.Count != 0
                        && (cT - _x469DataRecordTime > DATA_UPDATE_DIFF))
                    {
                        task.Work(_x469Data);
                    }
                    else
                    {
                        Add(new X469ValidDataTask());
                        Add(task);
                    }
                    break;
                case TaskType.X469_VALID_DATA:
                    if (_x469Data == null || _x469Data.Count == 0 
                        || (cT - _x469DataRecordTime > DATA_UPDATE_DIFF))
                    {
                        _waitType.Remove(type);
                        _workingType.Add(type);
                        LogUtil.Write("执行X469_VALID_DATA任务开始");
                        task.Work();
                        LogUtil.Write("执行X469_VALID_DATA任务结束");
                        _workingType.Remove(type);
                        if (task.GetData() != null)
                        {
                            _x469Data = task.GetData() as List<X469OddItem>;
                            _x469DataRecordTime = cT;
                        }
                        else
                        {
                            LogUtil.Write("X469_VALID_DATA获取不到数据，准备添加再来");
                            if (!_workingType.Contains(type)
                                    && !_waitType.Contains(type)
                                    && (FormMain.GetInstance().ContainsAccount(AcccountType.XPJ469)
                                    || FormMain.GetInstance().ContainsAccount(AcccountType.YL5789)))
                                Add(task);
                        }

                    }
                    break;
                case TaskType.X159_BET:
                    if (_x159Data != null && _x159Data.Count != 0
                        && (cT - _x159DataRecordTime <= DATA_UPDATE_DIFF))
                    {
                        task.Work(_x159Data);
                    }
                    else
                    {
                        Add(new X159ValidDataTask());
                        Add(task);
                    }
                    break;
                case TaskType.X159_VALID_DATA:
                    if (_x159Data == null || _x159Data.Count == 0
                        || (cT - _x159DataRecordTime > DATA_UPDATE_DIFF))
                    {
                        _waitType.Remove(type);
                        _workingType.Add(type);
                        task.Work();
                        _workingType.Remove(type);
                        if (task.GetData() != null)
                        {
                            LogUtil.Write("X159_VALID_DATA 数据添加成功");
                            _x159Data = task.GetData() as List<OddItem>;
                            _x159DataRecordTime = cT;
                        }
                        else
                        {
                            LogUtil.Write("X159_VALID_DATA获取不到数据，准备添加再来");
                            if (!_workingType.Contains(type)
                                    && !_waitType.Contains(type)
                                    && FormMain.GetInstance().ContainsAccount(AcccountType.XPJ155))
                                Add(task);
                        }

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
