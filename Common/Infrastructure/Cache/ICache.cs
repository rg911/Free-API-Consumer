using System;

namespace Common.Infrastructure.Cache
{
    /// <summary>
    /// Generic interface for caching data. It caches data into generic types.
    /// This interface can be impleted on both servics handler or web ui.
    /// In this application, it is only implemented on Web UI
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Default method adding key value cache
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="value">Cache vaule</param>
        void Add<T>(string key, T value);

        /// <summary>
        /// Cache add method with expire time
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="value">Cache vaule</param>
        /// <param name="expireAt">Expire time</param>
        void Add<T>(string key, T value, DateTime expireAt);

        /// <summary>
        /// Get cached by key
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Generic cached object</returns>
        T Get<T>(string key);

        /// <summary>
        /// Handle invalidate cache
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="key">Cache key</param>
        void Invalidate<T>(string key);
    }
}
