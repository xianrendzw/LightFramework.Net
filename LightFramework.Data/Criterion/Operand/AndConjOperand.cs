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

        public override string ToString()
        {
            return " AND ";
        }
    }
}
