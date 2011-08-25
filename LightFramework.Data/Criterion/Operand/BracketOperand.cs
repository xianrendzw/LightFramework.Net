using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class BracketOperand : DecoratorOperand
    {
        private Bracket _bracket;

        public BracketOperand(Bracket bracket)
        {
            this._bracket = bracket;
        }

        public override string ToString()
        {
            switch (this._bracket)
            {
                case Bracket.Bracket:
                    return string.Format("({0})", this._operand.ToString());
                case Bracket.Square:
                    return string.Format("[{0}]", this._operand.ToString());
                case Bracket.Curly:
                    return string.Format("{{{0}}}", this._operand.ToString());
                default:
                    return string.Format("({0})", this._operand.ToString());
            }
        }
    }
}
