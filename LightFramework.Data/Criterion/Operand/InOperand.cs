using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class InOperand : Operand
    {
        public InOperand()
            : base(new AndConj())
        {
        }

        public InOperand(IConj conj)
            : base(conj)
        {
        }

        public override string BuilderCondition(string columnName, string columnValue)
        {
            columnValue = columnValue.TrimEnd(',').TrimStart(',');

            return _conj.BuildCondition(string.Format("{0} IN ({1}) ", columnName, columnValue));
        }
    }
}
