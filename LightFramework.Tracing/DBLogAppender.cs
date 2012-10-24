using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace LightFramework.Tracing
{
    /// <summary>
    /// DBLogAppender提供把日志信息输出到数据库的相关方法与数据。
    /// </summary>
    public class DBLogAppender : ILogAppender
    {
        /// <summary>
        /// DBLogAppender的单件.
        /// </summary>
        public static readonly DBLogAppender Instance = new DBLogAppender();

        /// <summary>
        /// 构造函数。
        /// </summary>
        public DBLogAppender()
        {
        }

        /// <summary>
        /// 把日志信息输出到数据库的方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        public virtual void Append(MetaLog metaLog)
        {
            if (metaLog == null) return;

            DbMetaLog dblog = metaLog as DbMetaLog;
            if (dblog.FiledValues == null ||
                dblog.FiledValues.Count == 0) return;

            try
            {
                string connstr = ConfigurationManager.ConnectionStrings["logging"].ConnectionString;
                using (IDbConnection conn = this.CreateConnection(dblog.DbDialect, connstr))
                {
                    conn.Open();
                    IDbCommand cmd = this.CreateCommand(dblog, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("LightFramework.Tracing.DBLogAppender", ex.ToString());
            }
        }

        private IDbCommand CreateCommand(DbMetaLog dblog, IDbConnection conn)
        {
            string fields = "";
            string vals = "";
            DbParameter[] dbParameters = new DbParameter[dblog.FiledValues.Count];

            int i = 0;
            foreach (string key in dblog.FiledValues.Keys)
            {
                fields += string.Format("{0},", key);
                vals += string.Format("@{0},", key);
                dbParameters[i] = this.CreateDbParameter(dblog.DbDialect, "@" + key, dblog.FiledValues[key]);
                i++;
            }
            fields = fields.Trim(',');
            vals = vals.Trim(',');

            string commandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", dblog.DestTable, fields, vals);
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;
            this.AttachParameters(cmd, dbParameters);

            return cmd;
        }

        private void AttachParameters(IDbCommand command, DbParameter[] dbParameters)
        {
            if (command == null) 
                throw new ArgumentNullException("command");
            if (dbParameters == null) return;

            foreach (DbParameter p in dbParameters)
            {
                if ((p.Direction == ParameterDirection.InputOutput ||
                    p.Direction == ParameterDirection.Input) &&
                    (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }

        private DbConnection CreateConnection(DbDialect dbDialect, string connstr)
        {
            string typeName = string.Empty;
            if (dbDialect == DbDialect.MySql) 
                typeName = "MySql.Data.MySqlClient.MySqlConnection,MySql.Data";
            else if (dbDialect == DbDialect.SqlServer)
                typeName = "System.Data.SqlClient.SqlConnection,System.Data";
            else if (dbDialect == DbDialect.Oracle)
                typeName = "Oracle.DataAccess.Client.OracleConnection,Oracle.DataAccess";
            else if (dbDialect == DbDialect.Access)
                typeName = "System.Data.OleDb.OleDbConnection,System.Data";

            return (DbConnection)Activator.CreateInstance(Type.GetType(typeName), connstr);
        }

        private DbParameter CreateDbParameter(DbDialect dbDialect, string name, object value)
        {
            string typeName = string.Empty;
            if (dbDialect == DbDialect.MySql)
                typeName = "MySql.Data.MySqlClient.MySqlParameter,MySql.Data";
            else if (dbDialect == DbDialect.SqlServer)
                typeName = "System.Data.SqlClient.SqlParameter,System.Data";
            else if (dbDialect == DbDialect.Oracle)
                typeName = "Oracle.DataAccess.Client.OracleParameter,Oracle.DataAccess";
            else if (dbDialect == DbDialect.Access)
                typeName = "System.Data.OleDb.OleDbParameter,System.Data";

            return (DbParameter)Activator.CreateInstance(Type.GetType(typeName), name, value);
        }
    }
}
