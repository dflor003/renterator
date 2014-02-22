using System;
using System.Runtime.Caching;
using System.Threading;

namespace Renterator.Common.Caching
{
    public class Cache : ICache
    {
        private readonly ReaderWriterLockSlim lockObj = new ReaderWriterLockSlim();
        private readonly ObjectCache cache;

        public Cache()
            : this(MemoryCache.Default)
        {
        }

        public Cache(ObjectCache instance)
        {
            // Error check
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            cache = instance;
        }

        public static string Key(params object[] parts)
        {
            if (parts == null)
            {
                throw new ArgumentNullException("parts");
            }

            return string.Join(".", parts);
        }

        public TItem Get<TItem>(string cacheKey)
        {
            lockObj.EnterReadLock();
            try
            {
                return (TItem)cache.Get(cacheKey);
            }
            finally
            {
                lockObj.ExitReadLock();
            }
        }

        public bool TryGet<TItem>(string cacheKey, out TItem result)
        {
            lockObj.EnterReadLock();
            try
            {
                if (cache.Contains(cacheKey))
                {
                    result = (TItem)cache[cacheKey];
                    return true;
                }

                result = default(TItem);
                return false;
            }
            finally
            {
                lockObj.ExitReadLock();
            }
        }

        public TItem Get<TItem>(string cacheKey, CacheItemPolicy policy, Func<TItem> loadFunc)
        {
            return GetAndCacheInternal(cacheKey, loadFunc, value => Set(cacheKey, policy, value));
        }

        public TItem Get<TItem>(string cacheKey, DateTimeOffset expiration, Func<TItem> loadFunc)
        {
            return GetAndCacheInternal(cacheKey, loadFunc, value => Set(cacheKey, expiration, value));
        }

        public void Set(string cacheKey, DateTimeOffset expiration, object value)
        {
            WithWriteLock(() => cache.Set(cacheKey, value, expiration));
        }

        public void Set(string cacheKey, CacheItemPolicy policy, object value)
        {
            WithWriteLock(() => cache.Set(cacheKey, value, policy));
        }

        public void Remove(string cacheKey)
        {
            lockObj.EnterWriteLock();
            try
            {
                cache.Remove(cacheKey);
            }
            finally
            {
                lockObj.ExitWriteLock();
            }
        }

        private TItem GetAndCacheInternal<TItem>(string cacheKey, Func<TItem> loadFunc, Action<TItem> setFunc)
        {
            lockObj.EnterUpgradeableReadLock();
            try
            {
                if (!cache.Contains(cacheKey))
                {
                    TItem value = loadFunc();
                    setFunc(value);
                    return value;
                }
                else
                {
                    return (TItem)cache.Get(cacheKey);
                }
            }
            finally
            {
                lockObj.ExitUpgradeableReadLock();
            }
        }

        private void WithWriteLock(Action setFunc)
        {
            lockObj.EnterWriteLock();
            try
            {
                setFunc();
            }
            finally
            {
                lockObj.ExitWriteLock();
            }
        }
    }
}
