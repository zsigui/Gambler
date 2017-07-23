using Gambler.Module.HF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gambler.Utils.Manager
{
    public class LiveThreadManager
    {

        private static volatile LiveThreadManager instance;
        private static object syncObj = new object();

        private LiveThreadManager() { }

        public static LiveThreadManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new LiveThreadManager();
                        }
                    }
                }
                return instance;
            }
        }

        public const int MAX_THREAD_COUNT = 4;
        public Thread[] threads = new Thread[MAX_THREAD_COUNT];

        public volatile bool isRunning = false;
        private volatile HFClient _h8Client;


        public void Start(HFClient client)
        {
            if (client == null || client.LiveMatchs == null)
                return;
            _h8Client = client;
            
            if (isRunning)
                return;
            lock(syncObj)
            {
                if (isRunning)
                    return;
                isRunning = true;
                for (int i = 0; i < MAX_THREAD_COUNT; i++)
                {
                    if (threads[i] == null || !threads[i].IsAlive)
                    {
                        LogUtil.Write("线程开始： " + i);
                        threads[i] = ThreadUtil.RunOnThread(new ThreadStart(()=>{
                            while (isRunning)
                            {
                                try
                                {
                                    _h8Client.GetSpecLiveEvent(
                                    (eventData) =>
                                    {
                                        FormMain.GetInstance().UpdateLiveMatch(eventData, _h8Client.GetMatchById(eventData.MID.ToString()));
                                    }, null, null);
                                    if (_h8Client.IsEmptyLive())
                                    {
                                        Thread.Sleep(10);
                                    }
                                }
                                catch (Exception e)
                                {
                                    LogUtil.Write(e);
                                }
                            }
                        }));
                    }
                }
            }
        }

        public void Stop()
        {
            _h8Client = null;
            if (isRunning)
            {
                lock (syncObj)
                {
                    if (!isRunning)
                        return;

                    for (int i = 0; i < MAX_THREAD_COUNT; i++)
                    {
                        if (threads[i] != null && threads[i].IsAlive)
                        {
                            threads[i].Interrupt();
                            threads[i] = null;
                        }
                    }
                    isRunning = true;
                }
            }
        }
    }
}
