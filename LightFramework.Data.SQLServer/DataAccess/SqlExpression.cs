using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LightFramework.Data.SQLServer
{
    public class SqlExpression
    {
        public string CommandText;
        public SqlParameter[] Parameters;

        public SqlExpression(string commandText, SqlParameter[] parameters)
        {
            this.CommandText = commandText;
            this.Parameters = parameters;
        }
    }
}
