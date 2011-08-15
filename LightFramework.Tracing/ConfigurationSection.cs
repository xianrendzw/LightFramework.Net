using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ConfigurationSection类提供记录日志类的相关配置数据。
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
        /// 构造函数。
        /// </summary>
        public ConfigurationSection()
        {
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="appenderName">记录日志类的名称</param>
        /// <param name="fullTypeName">记录日志类的完成类名称</param>
        public ConfigurationSection(string appenderName,string fullTypeName)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="appenderName">记录日志类的名称</param>
        /// <param name="fullTypeName">记录日志类的完成类名称</param>
        /// <param name="encoding">记录日志时文本所用的字符编码类型名称</param>
        public ConfigurationSection(string appenderName, string fullTypeName,string encoding)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
            this._encoding = encoding;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="appenderName">记录日志类的名称</param>
        /// <param name="fullTypeName">记录日志类的完成类名称</param>
        /// <param name="encoding">记录日志时文本所用的字符编码类型名称</param>
        /// <param name="enabled">是否启动此记录日志类来提供记录日志的相关方法</param>
        public ConfigurationSection(string appenderName, string fullTypeName, string encoding,bool enabled)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
            this._encoding = encoding;
            this._enabled = enabled;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="appenderName">记录日志类的名称</param>
        /// <param name="fullTypeName">记录日志类的完成类名称</param>
        /// <param name="encoding">记录日志时文本所用的字符编码类型名称</param>
        /// <param name="enabled">是否启动此记录日志类来提供记录日志的相关方法</param>
        /// <param name="logPath">指定日志数据存储的路径,
        /// 说明:如果记录日志类为记录日志到数据库,则此处应该为数据库中的表名</param>
        public ConfigurationSection(string appenderName, string fullTypeName, string encoding, bool enabled,string logPath)
        {
            this._appenderName = appenderName;
            this._fullTypeName = fullTypeName;
            this._encoding = encoding;
            this._enabled = enabled;
            this._logPath = logPath;
        }

        /// <summary>
        /// 获取或设置记录日志类的名称
        /// </summary>
        public string AppenderName
        {
            get { return this._appenderName; }
            set { this._appenderName = value; }
        }

        /// <summary>
        /// 获取或设置记录日志类的完成类名称
        /// </summary>
        public string FullTypeName
        {
            get { return this._fullTypeName; }
            set { this._fullTypeName = value; }
        }

        /// <summary>
        /// 获取或设置记录日志时文本所用的字符编码类型名称
        /// </summary>
        public string Encoding
        {
            get { return this._encoding; }
            set { this._encoding = value; }
        }

        /// <summary>
        /// 获取或设置获取或设置是否启动此记录日志类来提供记录日志的相关方法
        /// </summary>
        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        /// <summary>
        /// 获取或设置指定日志数据存储的路径,
        /// 说明:如果记录日志类为记录日志到数据库,则此处应该为数据库中的表名
        /// </summary>
        public string LogPath
        {
            get { return this._logPath; }
            set { this._logPath = value; }
        }

        /// <summary>
        /// 获取或设置是否启用异步记录日志。默认为启用
        /// </summary>
        public bool IsAsyncWriteLog
        {
            get { return this._isAsyncWriteLog; }
            set { this._isAsyncWriteLog = value; }
        }
    }
}
