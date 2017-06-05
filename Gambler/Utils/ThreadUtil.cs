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

        /// <summary>
        /// 延迟指定时间执行任务
        /// </summary>
        /// <param name="method">执行任务</param>
        /// <param name="delayTimeInMillis">延迟时间，单位ms</param>
        public static void RunOnThreadDelay(ThreadStart method, int delayTimeInMillis)
        {
            RunOnThread(() => {
                Thread.Sleep(delayTimeInMillis);
                method.Invoke();
            });
        }

        /// <summary>
        /// 创建并启动定时器执行任务
        /// </summary>
        /// <param name="method">定时到达时执行的任务</param>
        /// <param name="parameter">传递给任务的对象参数</param>
        /// <param name="dueTime">延时启动定时器时间</param>
        /// <param name="period">定时器执行周期</param>
        /// <returns></returns>
        public static Timer RunOnTimer(TimerCallback method, object parameter, int dueTime, int period)
        {
            Timer timer = new Timer(method, parameter, dueTime, period);
            timer.Change(dueTime, period);
            return timer;
        }


        public static void WorkOnUI<T>(System.Windows.Forms.Control control, Delegate method, params T[] args)
        {
            if (control.IsDisposed)
                return;
            if (control.InvokeRequired)
            {
                if (args != null && args.Length > 0)
                {
                    switch (args.Length)
                    {
                        case 1:
                            control.Invoke(method, args[0]);
                            break;
                        case 2:
                            control.Invoke(method, args[0], args[1]);
                            break;
                        default:
                            control.Invoke(method, args);
                            break;

                    }
                }
                else
                {
                    control.Invoke(method);
                }
            }
            else
            {
                if (args != null && args.Length > 0)
                {
                    switch (args.Length)
                    {
                        case 1:
                            method.DynamicInvoke(args[0]);
                            break;
                        case 2:
                            method.DynamicInvoke(args[0], args[1]);
                            break;
                        default:
                            method.DynamicInvoke(args);
                            break;

                    }
                }
                else
                {
                    method.DynamicInvoke();
                }
            }
        }
    }
}
