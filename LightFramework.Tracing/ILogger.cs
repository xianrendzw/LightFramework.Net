using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ��¼��־�Ľӿڡ�
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// ��¼��־�ķ�����
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        void Write(MetaLog metaLog);

        /// <summary>
        /// ��¼��־�ķ���,�÷���Ĭ�ϰ���־��Ϣ��¼���ı��ļ���
        /// </summary>
        /// <param name="logMsg">��־�ı���Ϣ</param>
        void Write(string logMsg);

        /// <summary>
        /// ��¼��־�ķ���,�÷���Ĭ�ϰ���־��Ϣ��¼���ı��ļ���
        /// </summary>
        /// <param name="logMsg">��־�ı���Ϣ</param>
        /// <param name="logLevel">��־�ȼ�</param>
        void Write(string logMsg, LogLevel logLevel);
    }
}
