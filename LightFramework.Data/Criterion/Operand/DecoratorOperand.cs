using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public abstract class DecoratorOperand : Operand
    {
        protected Operand _operand = new NoneOperand();

        public virtual void Append(Operand operand)
        {
            this._operand = operand;
        }
    }
}
