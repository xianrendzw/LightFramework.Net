using System;
using System.Diagnostics;

namespace LightFramework.Core
{
    public static class DateTimeExtension
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        public static string DateDiff(this DateTime target)
        {
            string result = "";
            DateTime nowdt = DateTime.Now;
            TimeSpan ts = nowdt - target;

            if (ts.Days < 1)
            {
                if (ts.Hours < 1)
                {
                    if (ts.Minutes < 1)
                    {
                        result = ts.Seconds + "秒前";                        
                    }
                    else
                    {
                        result = ts.Minutes + "分钟前";
                    }
                }
                else if (ts.Hours < 24)
                {
                    result = ts.Hours + "小时前";
                }
            }
            else
            {
                result = ts.Days + "天前";
            }

            return result;
        }
    }
}