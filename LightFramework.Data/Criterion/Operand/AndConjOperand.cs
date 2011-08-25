using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class AndConjOperand : DecoratorOperand
    {
        public AndConjOperand()
        {
        }

        public override string ToString()
        {
            return string.Format(" AND ({0}) ",this._operand.ToString());
        }
    }
}
