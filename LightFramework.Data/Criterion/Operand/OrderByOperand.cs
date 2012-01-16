using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class OrderByOperand : Operand
    {
        private SortTypeEnum _sortType;
        private string[] _columnNames;

        public OrderByOperand(SortTypeEnum sortType, params string[] columnNames)
        {
            this._sortType = sortType;
            this._columnNames = columnNames;
        }

        protected override string ToExpression()
        {
            if (this._columnNames == null ||
                this._columnNames.Length <= 0)
            {
                return string.Empty;
            }

            return string.Format("ORDER BY {0} {1} ", string.Join(",", this._columnNames), this._sortType.ToString());
        }
    }
}
