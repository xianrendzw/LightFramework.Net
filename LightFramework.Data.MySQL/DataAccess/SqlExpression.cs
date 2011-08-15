using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace LightFramework.Data.MySQL
{
    public class SqlExpression
    {
        public string CommandText;
        public MySqlParameter[] Parameters;

        public SqlExpression(string commandText, MySqlParameter[] parameters)
        {
            this.CommandText = commandText;
            this.Parameters = parameters;
        }
    }
}
