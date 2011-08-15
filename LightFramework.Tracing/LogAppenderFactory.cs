using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// LogAppenderFactory�ṩ���������ļ�������־�Ĺ����ࡣ
    /// </summary>
    public class LogAppenderFactory
    {
        /// <summary>
        /// ��ȡ���ɾ����ļ�������־�Ķ���
        /// </summary>
        /// <param name="fullyTypeName">���ɾ����ļ�������־������������</param>
        /// <returns>���ɾ����ļ�������־�Ķ���</returns>
        public static ILogAppender Creator(string fullyTypeName)
        {
            //ILogAppender logAppender = null;
            //logAppender = (ILogAppender) new TextLogAppender();
            //return logAppender;
            return (ILogAppender)Activator.CreateInstance(Type.GetType(fullyTypeName));
        }
    }
}
