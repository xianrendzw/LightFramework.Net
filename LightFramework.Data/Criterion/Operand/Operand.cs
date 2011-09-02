using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public abstract class Operand
    {
        protected List<Operand> _operands;

        public virtual Operand Append(Operand operand)
        {
            if (this._operands == null)
                this._operands = new List<Operand>(5);

            this._operands.Add(operand);
            return this;
        }

        public override string ToString()
        {
            if (this._operands == null)
                return this.ToExpression();

            StringBuilder expr = new StringBuilder();
            expr.Append(this.ToExpression());

            foreach (Operand operand in this._operands)
            {
                expr.Append(operand.ToString());
            }

            return expr.ToString();
        }

        protected abstract string ToExpression();
    }
}
