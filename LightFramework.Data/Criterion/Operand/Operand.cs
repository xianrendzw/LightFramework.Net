using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public abstract class Operand
    {
        protected IConj _conj;

        protected Operand()
            : this(new AndConj())
        {
        }

        protected Operand(IConj conj)
        {
            this._conj = conj;
        }

        public abstract string BuilderCondition(string columnName, string columnValue);
    }
}
