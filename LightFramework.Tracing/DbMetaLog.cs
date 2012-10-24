using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// DbMetaLog类提供向数据库中封送日志的相关数据的类
    /// 该类可以根据需要扩展属性。
    /// </summary>
    public class DbMetaLog : MetaLog
    {
        private string _destTable;
        private string _connectionString = string.Empty;
        private Dictionary<string, object> _fieldvalues ;
        private DbDialect _dbDialect = DbDialect.MySql;

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="destTable">数据存储的目标表名称</param>
        public DbMetaLog(string destTable)
            : base()
        {
            this._storage = Storage.Db;
            this._destTable = destTable;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="logMsg">日志的文本信息</param>
        /// <param name="destTable">数据存储的目标表名称</param>
        /// <param name="dbDialect">数据库类型</param>
        public DbMetaLog(string logMsg, string destTable, DbDialect dbDialect)
            : base(logMsg)
        {
            this._storage = Storage.Db;
            this._destTable = destTable;
            this._dbDialect = dbDialect;
        }

        /// <summary>
        /// 获取或设置数据存储的目标表名称。
        /// </summary>
        public string DestTable
        {
            get { return this._destTable; }
            set { this._destTable = value; }
        }

        /// <summary>
        /// 获取或设置数据库连接字符串。
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
            set { this._connectionString = value; }
        }

        /// <summary>
        /// 获取或设置数据库字段与对应的值集合。
        /// </summary>
        public Dictionary<string, object> FiledValues
        {
            get { return this._fieldvalues; }
            set { this._fieldvalues = value; }
        }

        /// <summary>
        /// 获取或设置数据库类型。
        /// </summary>
        public DbDialect DbDialect
        {
            get { return this._dbDialect; }
            set { this._dbDialect = value; }
        }
    }
}
