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

        public override string ToString()
        {
            switch (this._bracket)
            {
                case Bracket.Bracket:
                    return string.Format("({0})");
                case Bracket.Square:
                    return string.Format("[{0}]");
                case Bracket.Curly:
                    return string.Format("{{{0}}}");
                default:
                    return string.Format("({0})");
            }
        }
    }
}
