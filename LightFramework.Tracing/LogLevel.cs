using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// LogLevelΪ�ṩ��־�������ز��������ݵ��ࡣ
    /// </summary>
    public class LogLevel
    {
        public static readonly LogLevel ALL = new LogLevel("ALL");
        public static readonly LogLevel DEBUG = new LogLevel("DEBUG");
        public static readonly LogLevel ERROR = new LogLevel("ERROR");
        public static readonly LogLevel FATAL = new LogLevel("FATAL");
        public static readonly LogLevel INFO = new LogLevel("INFO");
        public static readonly LogLevel OFF = new LogLevel("OFF");
        public static readonly LogLevel TRACE = new LogLevel("TRACE");
        public static readonly LogLevel WARN = new LogLevel("WARN");

        private string _logLevelName = "INFO";

        /// <summary>
        /// ���캯����
        /// </summary>
        public LogLevel()
        {

        }

        /// <summary>
        /// ���캯����
        /// </summary>
        public LogLevel(string logLevelName)
        {
            this._logLevelName = logLevelName;
        }

        /// <summary>
        /// ��ȡ��������־���������
        /// </summary>
        public string Name
        {
            get { return this._logLevelName; }
            set { this._logLevelName = value; }
        }
    }
}
