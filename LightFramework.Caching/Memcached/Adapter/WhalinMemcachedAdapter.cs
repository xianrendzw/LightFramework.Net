using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Caching.Memcached
{
    using Core;
    using Whalin.Caching;
    using Whalin.Caching.Memcached;

    public class WhalinMemcachedAdapter : ICache
    {
        private MemcachedClient _memcachedClient;

        public WhalinMemcachedAdapter()
            : this(MemcachedManager.CreateClient())
        {
        }

        public WhalinMemcachedAdapter(MemcachedClient memcachedClient)
		{
            this._memcachedClient = memcachedClient;
		}

        public T Get<T>(string key)
        {
            var cacheObject = this._memcachedClient.Get(key);
            return (cacheObject == null) ? default(T) : (T)cacheObject;
        }

        public T Get<T>(string key, Func<T> getObjectFunc)
        {
            var cacheObject = this._memcachedClient.Get(key);
            if (cacheObject == null)
            {
                cacheObject = getObjectFunc();
                this._memcachedClient.Set(key, cacheObject);
            }

            return (T)cacheObject;
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = default(T);
            return false;
        }

        public T Reload<T>(string key, Func<T> getObjectFunc)
        {
            this.Remove(key);
            T cacheObject = getObjectFunc();
            this._memcachedClient.Set(key, cacheObject);
            return cacheObject;
        }

        public int Count
        {
            get { return this._memcachedClient.Stats().Count; }
        }

        public void Clear()
        {
            this._memcachedClient.FlushAll();
        }

        public bool Contains(string key)
        {
            return this._memcachedClient.KeyExists(key);
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, DateTime.MaxValue);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            if (Contains(key))
                this._memcachedClient.Replace(key, value, absoluteExpiration);
            this._memcachedClient.Set(key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
        }

        public void Remove(string key)
        {
            if(this._memcachedClient.KeyExists(key))
                this._memcachedClient.Delete(key);
        }
    }
}
