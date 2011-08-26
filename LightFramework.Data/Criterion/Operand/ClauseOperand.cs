using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class ClauseOperand : DecoratorOperand
    {
        private SqlClause _sqlClause = SqlClause.None;

        public ClauseOperand(SqlClause sqlClause)
        {
            this._sqlClause = sqlClause;
        }

        public override string ToString()
        {
            string strSqlClause = this._sqlClause == SqlClause.None ? "" : this._sqlClause.ToString();
            return string.Format("{0} {1}", strSqlClause, this._operand.ToString());
        }
    }
}
