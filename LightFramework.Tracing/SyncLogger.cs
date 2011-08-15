using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LightFramework.Tracing
{
    /// <summary>
    /// SyncLogger类提供同步记录日志的相关数据与方法的类。
    /// </summary>
    public class SyncLogger : ILogger
    {
        /// <summary>
        /// SyncLogger的一个单件,支持多线程模式。
        /// </summary>
        public readonly static SyncLogger Instance = new SyncLogger();

        /// <summary>
        /// 私有构造函数。
        /// </summary>
        private SyncLogger()
        {
        }

        #region ILogger Members

        /// <summary>
        /// 记录日志的方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        public void Write(MetaLog metaLog)
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
        public void Write(string logMsg, LogLevel logLevel)
        {
            MetaLog metaLog = new TxtMetaLog(logMsg);
            metaLog.Level = logLevel;

            this.Write(metaLog);
        }
        #endregion
    }
}
