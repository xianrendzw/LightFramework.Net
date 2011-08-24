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
                case Bracket.LeftBracket:
                    return "(";
                case Bracket.LeftSquare:
                    return "[";
                case Bracket.LeftCurly:
                    return "{";
                case Bracket.RightBracket:
                    return ")";
                case Bracket.RightSquare:
                    return "]";
                case Bracket.RightCurly:
                    return "}";
                default:
                    return "";
            }
        }
    }
}
