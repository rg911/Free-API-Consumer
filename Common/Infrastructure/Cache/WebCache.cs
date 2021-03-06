﻿using System;
using System.Web;
using System.Web.Caching;

namespace Common.Infrastructure.Cache
{
    /// <summary>
    /// Implementation of ICache interface for Web UI
    /// </summary>
    public class WebCache : ICache
    {
        /// <summary>
        /// Get a new web cache instance
        /// </summary>
        private static System.Web.Caching.Cache Cache => HttpRuntime.Cache;

        #region Public Method
        public void Add<T>(string key, T value)
        {
            Cache[ConstructKey<T>(key)] = value;
        }

        public void Add<T>(string key, T value, DateTime expireAt)
        {
            key = ConstructKey<T>(key);
            Cache.Add(key, value, null, expireAt, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public T Get<T>(string key)
        {
            return (T)Cache[ConstructKey<T>(key)];
        }

        public void Invalidate<T>(string key)
        {
            Cache.Remove(ConstructKey<T>(key));
        }
        #endregion

        #region Privte Method
        private string ConstructKey<T>(string key)
        {
            var type = typeof(T).FullName;
            return $"{type}_{key}";
        }
        #endregion
    }
}