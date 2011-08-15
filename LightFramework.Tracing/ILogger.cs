using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LightFramework.Tracing
{
    /// <summary>
    /// 记录日志的接口。
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录日志的方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        void Write(MetaLog metaLog);

        /// <summary>
        /// 记录日志的方法,该方法默认把日志信息记录到文本文件。
        /// </summary>
        /// <param name="logMsg">日志文本信息</param>
        void Write(string logMsg);

        /// <summary>
        /// 记录日志的方法,该方法默认把日志信息记录到文本文件。
        /// </summary>
        /// <param name="logMsg">日志文本信息</param>
        /// <param name="logLevel">日志等级</param>
        void Write(string logMsg, LogLevel logLevel);
    }
}
