using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    class ThreadUtil
    {
        public static void RunOnThread(ThreadStart method)
        {
            Thread thread = new Thread(method);
            thread.IsBackground = true;
            thread.Start();
        }

        public static void RunOnThread(ThreadStart method, object parameter)
        {
            Thread thread = new Thread(method);
            thread.IsBackground = true;
            thread.Start(parameter);
        }

        public static void RunBgWorker(DoWorkEventHandler workHandler, RunWorkerCompletedEventHandler afterWorkHandler)
        {
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += afterWorkHandler;
                bw.DoWork += workHandler;
                bw.RunWorkerAsync();
            }
        }
    }
}
