using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// Logger��Ϊ��¼��־�Ĺ����ࡣ
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// ���캯����
        /// </summary>
        public Logger()
        {
        }

        /// <summary>
        /// ��ȡ�����þ����¼��־�Ķ���
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                //���������첽д��־
                if (ConfigurationSection.Instance.IsAsyncWriteLog)
                {
                    return (ILogger)AsyncLogger.Instance;
                }
                //��������ͬ��д��־
                else
                {
                    return (ILogger)SyncLogger.Instance;
                }
            }
        }
    }
}
