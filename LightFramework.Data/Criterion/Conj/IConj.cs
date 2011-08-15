using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    public interface IConj
    {
        string BuildCondition(string condition);
    }
}
