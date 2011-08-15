using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LightFramework.Tracing
{
    /// <summary>
    /// SyncLogger���ṩͬ����¼��־����������뷽�����ࡣ
    /// </summary>
    public class SyncLogger : ILogger
    {
        /// <summary>
        /// SyncLogger��һ������,֧�ֶ��߳�ģʽ��
        /// </summary>
        public readonly static SyncLogger Instance = new SyncLogger();

        /// <summary>
        /// ˽�й��캯����
        /// </summary>
        private SyncLogger()
        {
        }

        #region ILogger Members

        /// <summary>
        /// ��¼��־�ķ�����
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        public void Write(MetaLog metaLog)
        {
            ILogAppender logAppender = null;

            if (metaLog.Storage == Storage.Txt)
            {
                logAppender = TextLogAppender.Instance;
            }
            else if (metaLog.Storage == Storage.Db)
            {
                logAppender = DBLogAppender.Instance;
            }

            //����־��¼������Ĵ洢�����С�
            logAppender.Append(metaLog);
        }

        /// <summary>
        /// ��¼��־�ķ���,�÷���Ĭ�ϰ���־��Ϣ��¼���ı��ļ���
        /// </summary>
        /// <param name="logMsg">��־�ı���Ϣ</param>
        public void Write(string logMsg)
        {
            MetaLog metaLog = new TxtMetaLog(logMsg);
            this.Write(metaLog);
        }

        /// <summary>
        /// ��¼��־�ķ���,�÷���Ĭ�ϰ���־��Ϣ��¼���ı��ļ���
        /// </summary>
        /// <param name="logMsg">��־�ı���Ϣ</param>
        /// <param name="logLevel">��־�ȼ�</param>
        public void Write(string logMsg, LogLevel logLevel)
        {
            MetaLog metaLog = new TxtMetaLog(logMsg);
            metaLog.Level = logLevel;

            this.Write(metaLog);
        }
        #endregion
    }
}
