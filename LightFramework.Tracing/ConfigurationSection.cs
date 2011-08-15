using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ConfigurationSection���ṩ��¼��־�������������ݡ�
    /// </summary>
    public class ConfigurationSection
    {
        private string _appenderName = string.Empty;
        private string _fullTypeName = string.Empty;
        private string _encoding = "utf-8";
        private bool _enabled = false;
        private string _logPath = string.Empty;
        private bool _isAsyncWriteLog = true;

        public static readonly ConfigurationSection Instance = new ConfigurationSection();

        /// <summary>
        /// ���캯����
        /// </summary>
        public ConfigurationSection()
        {
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="appenderName">��¼��־�������</param>
        /// <param name="fullTypeName">��¼��־������������</param>
        public ConfigurationSection(string appenderName,string fullTypeName)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="appenderName">��¼��־�������</param>
        /// <param name="fullTypeName">��¼��־������������</param>
        /// <param name="encoding">��¼��־ʱ�ı����õ��ַ�������������</param>
        public ConfigurationSection(string appenderName, string fullTypeName,string encoding)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
            this._encoding = encoding;
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="appenderName">��¼��־�������</param>
        /// <param name="fullTypeName">��¼��־������������</param>
        /// <param name="encoding">��¼��־ʱ�ı����õ��ַ�������������</param>
        /// <param name="enabled">�Ƿ������˼�¼��־�����ṩ��¼��־����ط���</param>
        public ConfigurationSection(string appenderName, string fullTypeName, string encoding,bool enabled)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
            this._encoding = encoding;
            this._enabled = enabled;
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="appenderName">��¼��־�������</param>
        /// <param name="fullTypeName">��¼��־������������</param>
        /// <param name="encoding">��¼��־ʱ�ı����õ��ַ�������������</param>
        /// <param name="enabled">�Ƿ������˼�¼��־�����ṩ��¼��־����ط���</param>
        /// <param name="logPath">ָ����־���ݴ洢��·��,
        /// ˵��:�����¼��־��Ϊ��¼��־�����ݿ�,��˴�Ӧ��Ϊ���ݿ��еı���</param>
        public ConfigurationSection(string appenderName, string fullTypeName, string encoding, bool enabled,string logPath)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
            this._encoding = encoding;
            this._enabled = enabled;
            this._logPath = logPath;
        }

        /// <summary>
        /// ��ȡ�����ü�¼��־�������
        /// </summary>
        public string AppenderName
        {
            get { return this._appenderName; }
            set { this._appenderName = value; }
        }

        /// <summary>
        /// ��ȡ�����ü�¼��־������������
        /// </summary>
        public string FullTypeName
        {
            get { return this._fullTypeName; }
            set { this._fullTypeName = value; }
        }

        /// <summary>
        /// ��ȡ�����ü�¼��־ʱ�ı����õ��ַ�������������
        /// </summary>
        public string Encoding
        {
            get { return this._encoding; }
            set { this._encoding = value; }
        }

        /// <summary>
        /// ��ȡ�����û�ȡ�������Ƿ������˼�¼��־�����ṩ��¼��־����ط���
        /// </summary>
        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        /// <summary>
        /// ��ȡ������ָ����־���ݴ洢��·��,
        /// ˵��:�����¼��־��Ϊ��¼��־�����ݿ�,��˴�Ӧ��Ϊ���ݿ��еı���
        /// </summary>
        public string LogPath
        {
            get { return this._logPath; }
            set { this._logPath = value; }
        }

        /// <summary>
        /// ��ȡ�������Ƿ������첽��¼��־��Ĭ��Ϊ����
        /// </summary>
        public bool IsAsyncWriteLog
        {
            get { return this._isAsyncWriteLog; }
            set { this._isAsyncWriteLog = value; }
        }
    }
}
