using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class ClauseOperand : Operand
    {
        private SqlClause _sqlClause = SqlClause.None;
        private Operand _operand;

        public ClauseOperand(SqlClause sqlClause)
        {
            this._sqlClause = sqlClause;
            //this._operand = operand;
        }

        public override string ToString()
        {
            return this._sqlClause == SqlClause.None ? "" : this._sqlClause.ToString();
        }
    }
}
