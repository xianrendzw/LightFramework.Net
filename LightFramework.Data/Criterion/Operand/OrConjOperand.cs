using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class OrConjOperand : Operand
    {
        public OrConjOperand()
        {
        }

        public override string ToString()
        {
            return " OR ";
        }
    }
}
