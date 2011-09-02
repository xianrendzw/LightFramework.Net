using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class BracketOperand : Operand
    {
        private Bracket _bracket;

        public BracketOperand(Bracket bracket)
        {
            this._bracket = bracket;
        }

        protected override string ToExpression()
        {
            switch (this._bracket)
            {
                case Bracket.Left:
                    return "(";
                case Bracket.Rgiht:
                    return ")";
                default:
                    return string.Empty;
            }
        }
    }
}
