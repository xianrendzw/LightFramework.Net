using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class AndConj : IConj
    {
        public string BuildCondition(string condition)
        {
            return string.Format(" AND {0} ", condition);
        }
    }
}
