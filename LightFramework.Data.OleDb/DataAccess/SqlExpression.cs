using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace LightFramework.Data.OleDb
{
    public class SqlExpression
    {
        public string CommandText;
        public OleDbParameter[] Parameters;

        public SqlExpression(string commandText, OleDbParameter[] parameters)
        {
            this.CommandText = commandText;
            this.Parameters = parameters;
        }
    }
}
