﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class LessThanOperand : DecoratorOperand
    {
        private string _columnName;
        private object _columnValue;

        public LessThanOperand(string columnName, object columnValue)
        {
            this._columnName = columnName;
            this._columnValue = columnValue;
        }

        public override string ToString()
        {
            return string.Format("{0} < {1} {2} ", 
                this._columnName, this._columnValue,this._operand.ToString());
        }
    }
}
