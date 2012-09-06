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
                return AsyncLogger.Instance;
            }
        }
    }
}
