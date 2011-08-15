using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class GreaterThanOrEqualOperand : Operand
    {
        public GreaterThanOrEqualOperand()
            : base(new AndConj())
        {
        }

        public GreaterThanOrEqualOperand(IConj conj)
            : base(conj)
        {
        }

        public override string BuilderCondition(string columnName, string columnValue)
        {
            return _conj.BuildCondition(string.Format("{0} >= '{1}'", columnName, columnValue));
        }
    }
}
