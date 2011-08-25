using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class BetweenOperand : DecoratorOperand
    {
        private string _columnName;
        private object _lowerValue;
        private object _higherValue;

        public BetweenOperand(string columnName, object lowerValue, object higherValue)
        {
            this._columnName = columnName;
            this._lowerValue = lowerValue;
            this._higherValue = higherValue;
        }

        public override string ToString()
        {
            return string.Format("{0} BETWEEN {1} AND {2} {3} ",
                this._columnName, this._lowerValue, this._higherValue,this._operand.ToString());
        }
    }
}
