using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ILogLayout提供设置日志输出到设备中的版式的接口。
    /// </summary>
    public interface ILogLayout
    {
        /// <summary>
        /// 设置日志输出到设备中的版式
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        /// <returns>输出到设备中的文本格式</returns>
        string Format(MetaLog metaLog);

    }
}
