using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Caching.Memcached
{
    using Core;
    using Enyim.Caching;
    using Enyim.Caching.Memcached;

    public class EnyimMemcachedAdapter : ICache
    {
        private MemcachedClient _memcachedClient;

        public EnyimMemcachedAdapter()
            : this(new MemcachedClient())
        {
        }

        public EnyimMemcachedAdapter(MemcachedClient memcachedClient)
		{
            this._memcachedClient = memcachedClient;
		}

        public T Get<T>(string key)
        {
            return this._memcachedClient.Get<T>(key);
        }

        public T Get<T>(string key, Func<T> getObjectFunc)
        {
            var cacheObject = this._memcachedClient.Get(key);
            if (cacheObject == null)
            {
                cacheObject = getObjectFunc();
                this._memcachedClient.Store(StoreMode.Add, key, cacheObject);
            }

            return (T)cacheObject;
        }

        public bool TryGet<T>(string key, out T value)
        {
            value = default(T);
            object tmp = value;
            return this._memcachedClient.TryGet(key, out tmp);
        }

        public T Reload<T>(string key, Func<T> getObjectFunc)
        {
            this.Remove(key);
            T cacheObject = getObjectFunc();
            this._memcachedClient.Store(StoreMode.Set,key, cacheObject);
            return cacheObject;
        }

        public int Count
        {
            get { return 0; }
        }

        public void Clear()
        {
            this._memcachedClient.FlushAll();
        }

        public bool Contains(string key)
        {
            return (this._memcachedClient.Get(key) != null);
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, DateTime.MaxValue);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            if (this.Contains(key))
                this._memcachedClient.Store(StoreMode.Replace, key, value, absoluteExpiration);
            this._memcachedClient.Store(StoreMode.Add, key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
        }

        public void Remove(string key)
        {
            this._memcachedClient.Remove(key);
        }
    }
}
