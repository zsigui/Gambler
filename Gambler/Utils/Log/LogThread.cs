using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gambler.Utils.Log
{
    class LogThread
    {
        // Log 单例
        private static LogThread sSingleThread;
        private static object obj = new object();

        public static LogThread GetOrInit()
        {
            if (sSingleThread == null)
            {
                lock (obj)
                {
                    if (sSingleThread == null)
                    {
                        sSingleThread = new LogThread();
                    }
                }
            }
            return sSingleThread;
        }

        // 默认Log队列最大缓存数量
        private readonly int MAX_CACHE_LOG_COUNT = 100;
        private Thread workThread;
        private BlockingQueue<LogMessage> workQueue;
        private bool isStop = false;

        private static bool IsFinishConfig = false;

        private LogThread()
        {
            workQueue = new BlockingQueue<LogMessage>(MAX_CACHE_LOG_COUNT);
            workThread = new Thread(Work);
            workThread.IsBackground = true;
            workThread.Start();

        }


        private void RealWrite(Type type, string logContent, LogUtil.Log4NetLevel log4Level, Exception exception)
        {
            if (!IsFinishConfig)
                return;

            ILog log = type == null ? LogManager.GetLogger("") : LogManager.GetLogger(type);

            switch (log4Level)
            {
                case LogUtil.Log4NetLevel.Debug:
                    if (exception == null)
                        log.Debug(logContent);
                    else
                        log.Debug(logContent, exception);
                    break;
                case LogUtil.Log4NetLevel.Info:
                    if (exception == null)
                        log.Info(logContent);
                    else
                        log.Info(logContent, exception);
                    break;
                case LogUtil.Log4NetLevel.Warn:
                    if (exception == null)
                        log.Warn(logContent);
                    else
                        log.Warn(logContent, exception);
                    break;
                case LogUtil.Log4NetLevel.Error:
                    if (exception == null)
                        log.Error(logContent);
                    else
                        log.Error(logContent, exception);
                    break;
                case LogUtil.Log4NetLevel.Fatal:
                    if (exception == null)
                        log.Fatal(logContent);
                    else
                        log.Fatal(logContent, exception);
                    break;
            }
        }

        private void Work()
        {
            LogMessage message;

            while (!isStop)
            {
                message = workQueue.Dequeue();
                RealWrite(message.Type, message.Msg, message.Level, message.Exception);
            }
        }

        public void Initial()
        {
            if (!IsFinishConfig)
            {
                ThreadUtil.RunOnThread(() =>
                {
                    if (!IsFinishConfig)
                    {
                        log4net.Config.XmlConfigurator.Configure();
                        IsFinishConfig = true;
                    }
                });
            }
        }

        public void Destroy(bool interrupt)
        {
            isStop = true;
            if (interrupt)
            {
                workThread.Interrupt();
                workThread = null;
            }
            sSingleThread = null;
        }

        public void Write(Type type, string logContent, LogUtil.Log4NetLevel log4Level, Exception exception)
        {
            workQueue.Enqueue(new LogMessage(type, logContent, log4Level, exception));
        }

    }

    class LogMessage
    {
        private Type type;
        private string msg;
        private LogUtil.Log4NetLevel level;
        private Exception exception;


        public LogMessage(Type type, string msg, LogUtil.Log4NetLevel level, Exception exception)
        {
            this.Type = type;
            this.Msg = msg;
            this.Level = level;
            this.Exception = exception;
        }

        #region 属性
        public Type Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string Msg
        {
            get
            {
                return msg;
            }

            set
            {
                msg = value;
            }
        }

        public LogUtil.Log4NetLevel Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public Exception Exception
        {
            get
            {
                return exception;
            }

            set
            {
                exception = value;
            }
        }
        #endregion
    }
}
