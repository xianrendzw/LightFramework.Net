using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class GroupByOperand : Operand
    {
        private string[] _columnNames;

        public GroupByOperand(params string[] columnNames)
        {
            this._columnNames = columnNames;
        }

        protected override string ToExpression()
        {
            if (this._columnNames == null ||
                this._columnNames.Length <= 0)
            {
                return string.Empty;
            }

            return string.Format("GROUP BY {0} ", string.Join(",", this._columnNames));
        }
    }
}
