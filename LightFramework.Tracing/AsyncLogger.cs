using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Threading;

namespace LightFramework.Tracing
{
    /// <summary>
    /// AsyncLoggerΪ�첽��¼��־���ࡣ
    /// </summary>
    public class AsyncLogger : ILogger
    {
        /// <summary>
        /// AsyncLogger��һ������,֧�ֶ��߳�ģʽ��
        /// </summary>
        public readonly static AsyncLogger Instance = new AsyncLogger();

        /// <summary>
        /// ��¼��־���߳�
        /// </summary>
        private static Thread _writeLogThread = null;

        /// <summary>
        /// ��־����
        /// </summary>
        private static Queue<MetaLog> _logQueue = null;

        /// <summary>
        /// ˽�й��캯����
        /// </summary>
        private AsyncLogger()
        {
            
        }

        /// <summary>
        /// ��̬���캯����
        /// </summary>
        static AsyncLogger()
        {
            //����һ���̣߳�ר������дӦ�ó�����־������
            _logQueue = new Queue<MetaLog>(100);
            _writeLogThread = new Thread(new ThreadStart(AsyncWriteLog));
            _writeLogThread.Name = "writelogThread";
            _writeLogThread.IsBackground = true;
            _writeLogThread.Start();
        }

        /// <summary>
        ///  �첽д��־�ķ�����
        /// </summary>
        private static void AsyncWriteLog()
        {
            while (true)
            {
                //�����ǰ�����е���־
                WriteLogFactory(PopMetaLog());
            }
        }

        /// <summary>
        /// ���������ļ�������־�Ĺ���������
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        private static void WriteLogFactory(MetaLog metaLog)
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
        /// ͬ��MetaLog������ӷ�����
        /// </summary>
        /// <returns>MetaLog����</returns>
        private static MetaLog PopMetaLog()
        {
            lock (_logQueue)
            {
                if (_logQueue.Count == 0)
                {
                    //��ֹ�����̡߳�
                    Monitor.Wait(_logQueue);
                }

                //����MetaLog����
                return _logQueue.Dequeue();
            }
        }

        /// <summary>
        /// ͬ��MetaLog������ӷ�����
        /// </summary>
        /// <param name="metaLog">MetaLog����</param>
        private static void PushMetaLog(MetaLog metaLog)
        {
            lock (_logQueue)
            {
                //MetaLog�������
                _logQueue.Enqueue(metaLog);
                //֪ͨ�ȴ������̡߳�
                Monitor.PulseAll(_logQueue);
            }
        }

        #region ILogger Members

        /// <summary>
        /// ��¼��־�ķ�����
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        public void Write(MetaLog metaLog)
        {
            PushMetaLog(metaLog); 
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
        public void Write(string logMsg,LogLevel logLevel)
        {
            MetaLog metaLog = new TxtMetaLog(logMsg);
            metaLog.Level = logLevel;

            this.Write(metaLog);
        }

        /// <summary>
        /// �����־���������־��
        /// </summary>
        public void Clear()
        {
            lock (_logQueue)
            {
                while (_logQueue.Count > 0)
                {
                    WriteLogFactory(_logQueue.Dequeue());
                }
            }
        }

        #endregion
    }
}
