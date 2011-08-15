using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ILogAppender为把日志信息输出在相关设备的接口
    /// </summary>
    public interface ILogAppender
    {
        /// <summary>
        /// 把日志信息输出在相关设备方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        void Append(MetaLog metaLog);
    }
}
