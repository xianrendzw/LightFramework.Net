﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class NotEqualOperand : Operand
    {
        public NotEqualOperand()
            : base(new AndConj())
        {
        }

        public NotEqualOperand(IConj conj)
            : base(conj)
        {
        }

        public override string BuilderCondition(string columnName, string columnValue)
        {
            return _conj.BuildCondition(string.Format("{0} <> '{1}'", columnName, columnValue));
        }
    }
}