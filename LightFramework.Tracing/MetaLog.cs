using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace LightFramework.Tracing
{
    /// <summary>
    /// MetaLog类提供封送日志的相关数据的基类。
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
        /// 构造函数。
        /// </summary>
        public MetaLog()
        {
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="logMsg">日志的文本信息</param>
        public MetaLog(string logMsg)
        {
            this._logMsg = logMsg;
        }

        /// <summary>
        /// 获取或设置日志的文本信息。
        /// </summary>
        public string Message
        {
            get { return this._logMsg; }
            set { this._logMsg = value; }
        }

        /// <summary>
        /// 获取或设置日志输出字符编码名称。
        /// </summary>
        public string Encoding
        {
            get { return this._encoding; }
            set { this._encoding = value; }
        }

        /// <summary>
        /// 获取或设置日志封送对象的日志等级。
        /// </summary>
        public LogLevel Level
        {
            get { return this._level; }
            set { this._level = value; }
        }

        /// <summary>
        /// 获取或设置日志封送对象的类型,0表示登录成功,1表示系统错误,2表示增加，3表示删除，4表示修改,255表示其他。
        /// </summary>
        public byte Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        /// <summary>
        /// 获取或设置日志源主机IP。
        /// </summary>
        public string IPAddress
        {
            get { return this._ipAddress; }
            set { this._ipAddress = value; }
        }

        /// <summary>
        /// 获取记录日志的日期时间。
        /// </summary>
        public DateTime LogDateTime
        {
            get { return System.DateTime.Now; }
        }

        /// <summary>
        /// 获取日志信息存储介质。
        /// </summary>
        public virtual Storage Storage
        {
            get { return this._storage; }
        }
    }
}
