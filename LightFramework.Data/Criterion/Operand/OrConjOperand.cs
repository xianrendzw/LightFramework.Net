using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class OrConjOperand : DecoratorOperand
    {
        public OrConjOperand()
        {
        }

        public override string ToString()
        {
            return string.Format(" OR ({0}) ",this._operand.ToString());
        }
    }
}
