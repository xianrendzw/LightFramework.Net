using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Threading;

namespace LightFramework.Tracing
{
    /// <summary>
    /// AsyncLogger为异步记录日志的类。
    /// </summary>
    public class AsyncLogger : ILogger
    {
        /// <summary>
        /// AsyncLogger的一个单件,支持多线程模式。
        /// </summary>
        public readonly static AsyncLogger Instance = new AsyncLogger();

        /// <summary>
        /// 记录日志的线程
        /// </summary>
        private static Thread _writeLogThread = null;

        /// <summary>
        /// 日志队列
        /// </summary>
        private static Queue<MetaLog> _logQueue = null;

        /// <summary>
        /// 私有构造函数。
        /// </summary>
        private AsyncLogger()
        {
            
        }

        /// <summary>
        /// 静态构造函数。
        /// </summary>
        static AsyncLogger()
        {
            //开启一个线程，专门用于写应用程序日志操作。
            _logQueue = new Queue<MetaLog>(100);
            _writeLogThread = new Thread(new ThreadStart(AsyncWriteLog));
            _writeLogThread.Name = "writelogThread";
            _writeLogThread.IsBackground = true;
            _writeLogThread.Start();
        }

        /// <summary>
        ///  异步写日志的方法。
        /// </summary>
        private static void AsyncWriteLog()
        {
            while (true)
            {
                //输出当前队列中的日志
                WriteLogFactory(PopMetaLog());
            }
        }

        /// <summary>
        /// 创建具体文件类型日志的工厂方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        private static void WriteLogFactory(MetaLog metaLog)
        {
            ILogAppender logAppender = null;

            if (metaLog.Storage == Storage.Txt)
            {
                logAppender = TextLogAppender.Instance;
            }
            else if (metaLog.Storage == Storage.Db)
            {
                logAppender = DBLogAppender.Instance;
            }

            //把日志记录到具体的存储介质中。
            logAppender.Append(metaLog);
        }

        /// <summary>
        /// 同步MetaLog对象出队方法。
        /// </summary>
        /// <returns>MetaLog对象</returns>
        private static MetaLog PopMetaLog()
        {
            lock (_logQueue)
            {
                if (_logQueue.Count == 0)
                {
                    //阻止出队线程。
                    Monitor.Wait(_logQueue);
                }

                //返回MetaLog对象。
                return _logQueue.Dequeue();
            }
        }

        /// <summary>
        /// 同步MetaLog对象入队方法。
        /// </summary>
        /// <param name="metaLog">MetaLog对象</param>
        private static void PushMetaLog(MetaLog metaLog)
        {
            lock (_logQueue)
            {
                //MetaLog对象入队
                _logQueue.Enqueue(metaLog);
                //通知等待出队线程。
                Monitor.PulseAll(_logQueue);
            }
        }

        #region ILogger Members

        /// <summary>
        /// 记录日志的方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        public void Write(MetaLog metaLog)
        {
            PushMetaLog(metaLog); 
        }

        /// <summary>
        /// 记录日志的方法,该方法默认把日志信息记录到文本文件。
        /// </summary>
        /// <param name="logMsg">日志文本信息</param>
        public void Write(string logMsg)
        {
            MetaLog metaLog = new TxtMetaLog(logMsg);
            this.Write(metaLog);
        }

        /// <summary>
        /// 记录日志的方法,该方法默认把日志信息记录到文本文件。
        /// </summary>
        /// <param name="logMsg">日志文本信息</param>
        /// <param name="logLevel">日志等级</param>
        public void Write(string logMsg,LogLevel logLevel)
        {
            MetaLog metaLog = new TxtMetaLog(logMsg);
            metaLog.Level = logLevel;

            this.Write(metaLog);
        }

        /// <summary>
        /// 清空日志队列里的日志。
        /// </summary>
        public void Clear()
        {
            lock (_logQueue)
            {
                while (_logQueue.Count > 0)
                {
                    WriteLogFactory(_logQueue.Dequeue());
                }
            }
        }

        #endregion
    }
}
