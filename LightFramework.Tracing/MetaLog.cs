using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LightFramework.Tracing
{
    /// <summary>
    /// MetaLog���ṩ������־��������ݵĻ��ࡣ
    /// </summary>
    public abstract class MetaLog
    {
        private string _logMsg = string.Empty;
        private string _encoding = "utf-8";
        private LogLevel _level = LogLevel.Info;
        private string _ipAddress = string.Empty;
        private byte _type;
        protected Storage _storage = Storage.Txt;

        /// <summary>
        /// ���캯����
        /// </summary>
        public MetaLog()
        {
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="logMsg">��־���ı���Ϣ</param>
        public MetaLog(string logMsg)
        {
            this._logMsg = logMsg;
        }

        /// <summary>
        /// ��ȡ��������־���ı���Ϣ��
        /// </summary>
        public string Message
        {
            get { return this._logMsg; }
            set { this._logMsg = value; }
        }

        /// <summary>
        /// ��ȡ��������־����ַ��������ơ�
        /// </summary>
        public string Encoding
        {
            get { return this._encoding; }
            set { this._encoding = value; }
        }

        /// <summary>
        /// ��ȡ��������־���Ͷ������־�ȼ���
        /// </summary>
        public LogLevel Level
        {
            get { return this._level; }
            set { this._level = value; }
        }

        /// <summary>
        /// ��ȡ��������־���Ͷ��������,0��ʾ��¼�ɹ�,1��ʾϵͳ����,2��ʾ���ӣ�3��ʾɾ����4��ʾ�޸�,255��ʾ������
        /// </summary>
        public byte Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        /// <summary>
        /// ��ȡ��������־Դ����IP��
        /// </summary>
        public string IPAddress
        {
            get { return this._ipAddress; }
            set { this._ipAddress = value; }
        }

        /// <summary>
        /// ��ȡ��¼��־������ʱ�䡣
        /// </summary>
        public DateTime LogDateTime
        {
            get { return System.DateTime.Now; }
        }

        /// <summary>
        /// ��ȡ��־��Ϣ�洢���ʡ�
        /// </summary>
        public virtual Storage Storage
        {
            get { return this._storage; }
        }
    }
}
