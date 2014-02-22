using System;
using System.Runtime.Caching;

namespace Renterator.Common.Caching
{
    public interface ICache
    {
        TItem Get<TItem>(string cacheKey);
        TItem Get<TItem>(string cacheKey, CacheItemPolicy policy, Func<TItem> loadFunc);
        TItem Get<TItem>(string cacheKey, DateTimeOffset expiration, Func<TItem> loadFunc);
        void Set(string key, DateTimeOffset expiration, object value);
        void Set(string key, CacheItemPolicy policy, object value);
        bool TryGet<TItem>(string cacheKey, out TItem result);
        void Remove(string cacheKey);
    }
}
