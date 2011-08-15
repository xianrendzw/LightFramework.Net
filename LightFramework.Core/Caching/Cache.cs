using System;
using System.Diagnostics;
using System.Configuration;
using System.Reflection;

namespace LightFramework.Core
{
    public static class Cache
    {
        private static ICache _internalCache;
        private static readonly object lockPad = new object();

        public static int Count
        {
            [DebuggerStepThrough]
            get
            {
                return InternalCache.Count;
            }
        }

        private static ICache InternalCache
        {
            get
            {
                //return IoC.Resolve<ICache>();
                if (_internalCache != null)
                {
                    return _internalCache;
                }

                lock (lockPad)
                {
                    if (_internalCache == null)
                    {
                        string typeName = ConfigurationManager.AppSettings["cache"];
                        _internalCache = (ICache)Activator.CreateInstance(Type.GetType(typeName));
                    }

                    return _internalCache;
                }
            }
        }

        [DebuggerStepThrough]
        public static void Clear()
        {
            InternalCache.Clear();
        }

        [DebuggerStepThrough]
        public static bool Contains(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.Contains(key);
        }

        [DebuggerStepThrough]
        public static T Get<T>(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.Get<T>(key);
        }

        [DebuggerStepThrough]
        public static bool TryGet<T>(string key, out T value)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.TryGet(key, out value);
        }

        [DebuggerStepThrough]
        public static void Set<T>(string key, T value)
        {
            Check.Argument.IsNotEmpty(key, "key");

            InternalCache.Set(key, value);
        }

        [DebuggerStepThrough]
        public static void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            Check.Argument.IsNotEmpty(key, "key");
            Check.Argument.IsNotInPast(absoluteExpiration, "absoluteExpiration");

            InternalCache.Set(key, value, absoluteExpiration);
        }

        [DebuggerStepThrough]
        public static void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Check.Argument.IsNotEmpty(key, "key");
            Check.Argument.IsNotNegativeOrZero(slidingExpiration, "absoluteExpiration");

            InternalCache.Set(key, value, slidingExpiration);
        }

        [DebuggerStepThrough]
        public static void Remove(string key)
        {
            Check.Argument.IsNotEmpty(key, "key");

            InternalCache.Remove(key);
        }

        //[DebuggerStepThrough]
        public static T Get<T>(string key, Func<T> getObjectFunc)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.Get(key, getObjectFunc);
        }

        [DebuggerStepThrough]
        public static T Reload<T>(string key, Func<T> getObjectFunc)
        {
            Check.Argument.IsNotEmpty(key, "key");

            return InternalCache.Reload(key, getObjectFunc);
        }
    }
}