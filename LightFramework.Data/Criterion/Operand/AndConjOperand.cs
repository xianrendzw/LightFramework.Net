using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class AndConjOperand : Operand
    {
        public AndConjOperand()
        {
        }

        protected override string ToExpression()
        {
            return " AND ";
        }
    }
}
