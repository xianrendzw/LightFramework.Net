using System.Collections.Generic;
using System.Diagnostics;

namespace LightFramework.Core
{
    public static class CollectionExtension
    {
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return (collection == null) || (collection.Count == 0);
        }
    }
}