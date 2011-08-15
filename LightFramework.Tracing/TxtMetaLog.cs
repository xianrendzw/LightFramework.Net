using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// TxtMetaLog类提供文本文件封送日志的相关数据的类。
    /// </summary>
    public class TxtMetaLog : MetaLog
    {
        private string _path = AppDomain.CurrentDomain.BaseDirectory;
        private string _logFileName = "log.txt";

        /// <summary>
        /// 构造函数。
        /// </summary>
        public TxtMetaLog()
            : base()
        {
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="logMsg">日志的文本信息</param>
        public TxtMetaLog(string logMsg)
            : base(logMsg)
        {
        }

        /// <summary>
        /// 获取或设置日志输出到设备的位置。
        /// </summary>
        public string Path
        {
            get { return this._path; }
            set { this._path = value; }
        }

        /// <summary>
        /// 获取或设置日志输出文件时的文件名称。
        /// </summary>
        public string LogFileName
        {
            get { return this._logFileName; }
            set { this._logFileName = value; }
        }
    }
}
