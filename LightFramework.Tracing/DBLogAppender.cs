using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LightFramework.Tracing
{
    /// <summary>
    /// DBLogAppender�ṩ����־��Ϣ�����Txt�����ı��ļ�����ط��������ݡ�
    /// </summary>
    public class DBLogAppender : ILogAppender
    {
        /// <summary>
        /// DBLogAppender�ĵ���.
        /// </summary>
        public static readonly DBLogAppender Instance = new DBLogAppender();

        /// <summary>
        /// ���캯����
        /// </summary>
        public DBLogAppender()
        {
        }

        /// <summary>
        /// ����־��Ϣ�����Txt�����ı��ļ��ķ�����
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        public virtual void Append(MetaLog metaLog)
        {
            if (metaLog == null) return;

            DbMetaLog dblog = metaLog as DbMetaLog;
            if (dblog.FiledValues == null ||
                dblog.FiledValues.Count == 0)
            {
                return;
            }

            string fields = "";
            string vals = "";
            SqlParameter[] parameters = new SqlParameter[dblog.FiledValues.Count];

            int i = 0;
            foreach (string key in dblog.FiledValues.Keys)
            {
                fields += string.Format("[{0}],", key);
                vals += string.Format("@{0},", key);
                parameters[i] = new SqlParameter("@" + key, dblog.FiledValues[key]);
                i++;
            }
            fields = fields.Trim(',');
            vals = vals.Trim(',');

            try
            {
                string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["hnce.log"].ConnectionString;
                string sqlCmd = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", dblog.DestTable, fields, vals);
                using (IDbConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sqlCmd;
                    cmd.CommandType = CommandType.Text;
                    this.AttachParameters(cmd, parameters);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("LightFramework.Tracing", ex.ToString());
            }
        }

        private void AttachParameters(IDbCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters == null)
            {
                return;
            }

            foreach (SqlParameter p in commandParameters)
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
    }
}
