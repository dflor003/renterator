using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Renterator.Common.Caching;

namespace Renterator.Services.Tests.Helpers
{
    public class InMemoryCache : Dictionary<string, object>, ICache
    {
        public TItem Get<TItem>(string cacheKey)
        {
            object result;
            if (this.TryGetValue(cacheKey, out result))
            {
                return (TItem)result;
            }

            return default(TItem);
        }

        public TItem Get<TItem>(string cacheKey, DateTimeOffset expiration, Func<TItem> loadFunc)
        {
            return GetInternal(cacheKey, loadFunc);
        }

        public TItem Get<TItem>(string cacheKey, CacheItemPolicy policy, Func<TItem> loadFunc)
        {
            return GetInternal(cacheKey, loadFunc);
        }

        public void Set(string key, DateTimeOffset expiration, object value)
        {
            this[key] = value;
        }

        public void Set(string key, CacheItemPolicy policy, object value)
        {
            this[key] = value;
        }

        public bool TryGet<TItem>(string cacheKey, out TItem result)
        {
            object objResult;
            bool hasItem = this.TryGetValue(cacheKey, out objResult);

            result = (TItem)objResult;
            return hasItem;
        }

        void ICache.Remove(string cacheKey)
        {
            Remove(cacheKey);
        }

        private TItem GetInternal<TItem>(string cacheKey, Func<TItem> loadFunc)
        {
            if (!this.ContainsKey(cacheKey))
            {
                TItem result = loadFunc();
                this[cacheKey] = result;
                return result;
            }

            return (TItem)this[cacheKey];
        }
    }
}
