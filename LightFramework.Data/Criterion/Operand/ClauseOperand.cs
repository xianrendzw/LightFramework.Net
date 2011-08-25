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
            return this._sqlClause == SqlClause.None ? "" : this._sqlClause.ToString();
        }
    }
}
