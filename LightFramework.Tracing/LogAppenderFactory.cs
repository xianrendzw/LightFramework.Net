using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// LogAppenderFactory提供创建具体文件类型日志的工厂类。
    /// </summary>
    public class LogAppenderFactory
    {
        /// <summary>
        /// 获取生成具体文件类型日志的对象。
        /// </summary>
        /// <param name="fullyTypeName">生成具体文件类型日志的类完整名称</param>
        /// <returns>生成具体文件类型日志的对象。</returns>
        public static ILogAppender Creator(string fullyTypeName)
        {
            //ILogAppender logAppender = null;
            //logAppender = (ILogAppender) new TextLogAppender();
            //return logAppender;
            return (ILogAppender)Activator.CreateInstance(Type.GetType(fullyTypeName));
        }
    }
}
