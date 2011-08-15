using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// 日志信息存储介质。
    /// </summary>
    public enum Storage
    {
        /// <summary>
        /// 文本文件
        /// </summary>
        Txt = 1,

        /// <summary>
        /// 数据库
        /// </summary>
        Db = 2,

        /// <summary>
        /// Xml文件
        /// </summary>
        Xml = 3
    }
}
