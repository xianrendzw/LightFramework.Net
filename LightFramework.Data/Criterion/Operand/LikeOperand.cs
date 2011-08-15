using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class LikeOperand : Operand
    {
        public LikeOperand()
            : base(new AndConj())
        {
        }

        public LikeOperand(IConj conj)
            : base(conj)
        {
        }

        public override string BuilderCondition(string columnName, string columnValue)
        {
            columnValue = columnValue.Replace("-", "[-]").Replace("%", "[%]");

            return _conj.BuildCondition(string.Format("{0} like '%{1}%'", columnName, columnValue));
        }
    }
}
