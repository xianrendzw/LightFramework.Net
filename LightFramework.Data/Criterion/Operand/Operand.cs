using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public abstract class Operand
    {
        protected List<Operand> _operands;

        public abstract override string ToString();

        public virtual Operand Append(Operand operand)
        {
            if (this._operands == null)
                this._operands = new List<Operand>(5);

            this._operands.Add(operand);
            return this;
        }

        public virtual string Compile()
        {
            if (this._operands == null)
                return this.ToString();

            StringBuilder expr = new StringBuilder();
            expr.Append(this.ToString());

            foreach (Operand operand in this._operands)
            {
                expr.Append(operand.Compile());
            }

            return expr.ToString();
        }
    }
}
