using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LightFramework.Tracing
{
    /// <summary>
    /// TextLogAppender�ṩ����־��Ϣ�����Txt�����ı��ļ�����ط��������ݡ�
    /// </summary>
    public class TextLogAppender : ILogAppender,ILogLayout
    {
        /// <summary>
        /// TextLogAppender�ĵ���.
        /// </summary>
        public static readonly TextLogAppender Instance = new TextLogAppender();

        /// <summary>
        /// ���캯����
        /// </summary>
        public TextLogAppender()
        {
        }

        /// <summary>
        /// ����־��Ϣ�����Txt�����ı��ļ��ķ�����
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        public virtual void Append(MetaLog metaLog)
        {
            TxtMetaLog txtlog = metaLog as TxtMetaLog;
            string filePath = txtlog.Path + @"log\" + txtlog.LogDateTime.ToShortDateString();

            //���Ŀ¼������,�򴴽�Ŀ¼
            if (!Directory.Exists(filePath)) 
                Directory.CreateDirectory(filePath);
            //������ʽ���ļ�·��
            string fullFileName = filePath + @"\" + txtlog.LogFileName;

            //д�ļ�
            StreamWriter sw = new StreamWriter(fullFileName, true, Encoding.GetEncoding(metaLog.Encoding));
            sw.WriteLine(this.Format(metaLog));
            sw.Close();
        }

        #region ILogLayout Members

        /// <summary>
        /// ������־������ı��ļ��еİ�ʽ��
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        /// <returns>������豸�е��ı���ʽ</returns>
        public string Format(MetaLog metaLog)
        {
            string format = "[����:{0}][ʱ��:{1}],[��Ϣ:{2}]\r\n";
            return string.Format(format, metaLog.Level.Name,metaLog.LogDateTime.ToString(), metaLog.Message);
        }

        #endregion
    }
}
