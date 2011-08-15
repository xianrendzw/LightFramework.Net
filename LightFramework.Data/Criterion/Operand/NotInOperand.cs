using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class NotInOperand : Operand
    {
        public NotInOperand()
            : base(new AndConj())
        {
        }

        public NotInOperand(IConj conj)
            : base(conj)
        {
        }

        public override string BuilderCondition(string columnName, string columnValue)
        {
            return _conj.BuildCondition(string.Format("{0} not in ({1})", columnName, columnValue));
        }
    }
}
