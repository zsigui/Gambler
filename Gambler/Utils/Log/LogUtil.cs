using Gambler.Utils.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class LogUtil
    {
        /// <summary>
        /// 程序开始时调用进行Log输出配置
        /// </summary>
        public static void Initial()
        {
            LogThread.GetOrInit().Initial();
        }

        /// <summary>
        /// 调用Log4net写日志，日志等级为 ：信息（Info）
        /// </summary>
        /// <param name="logContent">日志内容</param>
        public static void Write(string logContent)
        {
            Console.WriteLine(logContent);
            LogThread.GetOrInit().Write(null, logContent, Log4NetLevel.Info, null);
        }

        /// <summary>
        /// 调用Log4net写日志，日志等级为 ：信息（Info）
        /// </summary>
        /// <param name="e">抛出异常信息</param>
        public static void Write(Exception e)
        {
            
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            LogThread.GetOrInit().Write(null, null, Log4NetLevel.Info, e);
        }

        /// <summary>
        /// 调用Log4net写日志，日志等级为 ：信息（Info）
        /// </summary>
        /// <param name="type">类的类型，指定日志中错误的具体类。例如：typeof(Index)，Index是类名，如果为空表示不指定类</param>
        /// <param name="logContent">日志内容</param>
        public static void Write(Type type, string logContent)
        {
            LogThread.GetOrInit().Write(type, logContent, Log4NetLevel.Info, null);
        }

        /// <summary>
        /// 调用Log4net写日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="log4Level">记录日志等级，枚举</param>
        public static void Write(string logContent, Log4NetLevel log4Level)
        {
            LogThread.GetOrInit().Write(null, logContent, log4Level, null);
        }

        /// <summary>
        /// 调用Log4net写日志，日志等级为 ：错误（Error）
        /// </summary>
        /// <param name="type">类的类型，指定日志中错误的具体类。例如：typeof(Index)，Index是类名，如果为空表示不指定类</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="exception">抛出异常</param>
        public static void Write(Type type, string logContent, Exception exception)
        {
            LogThread.GetOrInit().Write(type, logContent, Log4NetLevel.Info, exception);
        }

        /// <summary>
        /// 调用Log4net写日志
        /// </summary>
        /// <param name="type">类的类型，指定日志中错误的具体类。例如：typeof(Index)，Index是类名，如果为空表示不指定类</param>
        /// <param name="logContent">日志内容</param>
        /// <param name="log4Level">记录日志等级，枚举</param>
        /// <param name="exception">抛出异常</param>
        public void Write(Type type, string logContent, LogUtil.Log4NetLevel log4Level, Exception exception)
        {
            LogThread.GetOrInit().Write(type, logContent, log4Level, exception);
        }

        /// <summary>
        /// log4net 日志等级类型枚举
        /// </summary>
        public enum Log4NetLevel
        {
            [Description("调试信息")]
            Debug = 1,
            [Description("一般信息")]
            Info = 2,
            [Description("警告信息")]
            Warn = 3,
            [Description("错误日志")]
            Error = 4,
            [Description("严重错误")]
            Fatal = 5

        }
    }
}
