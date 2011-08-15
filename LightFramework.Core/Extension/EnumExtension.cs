using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Core
{
    public static class EnumExtension
    {
        public static Dictionary<int, string> ToDictionary(this Enum target)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (int value in Enum.GetValues(target.GetType()))
            {
                dic.Add(value, Enum.GetName(target.GetType(), value));
            }

            return dic;
        }
    }
}
