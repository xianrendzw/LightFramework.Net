using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class ClauseOperand : Operand
    {
        private SqlClause _sqlClause = SqlClause.None;

        public ClauseOperand(SqlClause sqlClause)
        {
            this._sqlClause = sqlClause;
        }

        public override string ToString()
        {
            if (this._operands == null)
                this._sqlClause = SqlClause.None;

            return base.ToString();
        }

        protected override string ToExpression()
        {
            string strSqlClause = this._sqlClause == SqlClause.None ? "" : this._sqlClause.ToString();
            return string.Format("{0} ", strSqlClause);
        }
    }
}
