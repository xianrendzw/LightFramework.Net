using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public class OrConj : IConj
    {
        public string BuildCondition(string condition)
        {
            return string.Format(" OR {0} ", condition);
        }
    }
}
