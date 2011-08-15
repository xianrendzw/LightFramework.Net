using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// Logger类为记录日志的工厂类。
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Logger()
        {
        }

        /// <summary>
        /// 获取或设置具体记录日志的对象。
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                //设置启用异步写日志
                if (ConfigurationSection.Instance.IsAsyncWriteLog)
                {
                    return (ILogger)AsyncLogger.Instance;
                }
                //设置启用同步写日志
                else
                {
                    return (ILogger)SyncLogger.Instance;
                }
            }
        }
    }
}
