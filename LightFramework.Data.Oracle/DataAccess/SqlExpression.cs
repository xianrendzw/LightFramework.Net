using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace LightFramework.Data.Oracle
{
    public class SqlExpression
    {
        public string CommandText;
        public OracleParameter[] Parameters;

        public SqlExpression(string commandText, OracleParameter[] parameters)
        {
            this.CommandText = commandText;
            this.Parameters = parameters;
        }
    }
}
