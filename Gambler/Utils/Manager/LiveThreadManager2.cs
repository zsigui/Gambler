using Gambler.Module.HF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gambler.Utils.Manager
{
    public class LiveThreadManager2
    {

        private static volatile LiveThreadManager2 instance;
        private static object syncObj = new object();

        private LiveThreadManager2() { }

        public static LiveThreadManager2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new LiveThreadManager2();
                        }
                    }
                }
                return instance;
            }
        }

        private Dictionary<string, Thread> _runningThreads = new Dictionary<string, Thread>();
        private HFClient _h8Client = null;

        public Thread ObtainThread(string key)
        {
            if (_runningThreads == null)
                return null;
            Thread result = null;
            _runningThreads.TryGetValue(key, out result);
            return result;
        }

        public void PutThreadSync(string key, Thread value)
        {
            Thread t = ObtainThread(key);
            if (t == null)
            {
                lock (syncObj)
                {
                    _runningThreads.Add(key, value);
                }
            }
        }

        public void RemoveThreadSync(string key)
        {
            Thread t = ObtainThread(key);
            if (t != null)
            {
                lock (syncObj)
                {
                    _runningThreads.Remove(key);
                }
            }
        }

        public void Start(HFClient client)
        {
            if (client == null)
                return;

            _h8Client = client;
            if ((_h8Client.LiveMatchs == null ||
                _h8Client.LiveMatchs.Count == 0))
            {
                if (_runningThreads.Count != 0)
                    Stop();
                else
                    return;
            }

            Stop();
            Thread tmp;
            foreach (string matchId in _h8Client.LiveMatchs.Keys)
            {
                tmp = ObtainThread(matchId);
                if (tmp == null)
                {
                    Thread newT = ThreadUtil.RunOnThread(new ThreadStart(() =>
                    {
                        try
                        {
                            int eid = -1;
                            int subFrequent = 1;
                            while (true)
                            {
                                if (!FormMain.GetInstance()._uncheckedList.Contains(matchId))
                                {
                                    LogUtil.Write("请求MatchId = " + matchId + ", 当前eid = " + eid);
                                    subFrequent = 1;
                                    _h8Client.GetSpecLiveEvent(
                                            matchId, eid,
                                            (eventData) =>
                                            {
                                                eid = eventData.EID;
                                                FormMain.GetInstance().UpdateLiveMatch(eventData, _h8Client.GetMatchById(eventData.MID.ToString()));
                                            },
                                            (hc, ec, em) =>
                                            {
                                                LogUtil.Write("(1, 2, 3) 请求失败： 请求MatchId = " + matchId + ", 当前eid = " + eid);
                                            },
                                            (e) =>
                                            {
                                                LogUtil.Write("(1) 请求出错： 请求MatchId = " + matchId + ", 当前eid = " + eid);
                                            });
                                    Thread.Sleep(500);
                                }
                                else
                                {
                                    if (subFrequent < 6)
                                        subFrequent += 1;
                                    Thread.Sleep(500 * subFrequent);
                                }
                            }
                        }
                        catch (ThreadInterruptedException)
                        {
                            //LogUtil.Write(e);
                        }
                    }));
                    PutThreadSync(matchId, newT);
                }
            }
        }

        public void Stop()
        {
            lock(syncObj)
            {
                foreach (Thread t in _runningThreads.Values)
                {
                    if (t.IsAlive)
                    {
                        t.Interrupt();
                        t.Abort();
                    }
                }
                _runningThreads.Clear();
            }
        }
    }
}
