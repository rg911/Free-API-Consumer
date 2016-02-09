using System;
using System.Threading.Tasks;
using Common.Enums;
using Common.Infrastructure.Api;
using Common.Infrastructure.Cache;
using Common.Repository.Interfaces;
using Common.ViewModel;
using log4net;

namespace Common.Repository.Implementations
{
    /// <summary>
    /// Implementation of IRatingKeyRepository
    /// </summary>
    public class RatingKeyRepository : IRatingKeyRepository
    {
        #region Properties
        /// <summary>
        /// Local API service property
        /// </summary>
        private IApi<RatingKeyViewModel> Api { get; }
        /// <summary>
        /// Web cache
        /// </summary>
        private ICache Cache { get; }
        /// <summary>
        /// Log4Net 
        /// </summary>
        private ILog Log { get; }
        #endregion

        #region Constructor

        /// <summary>
        /// Inject API dependency to constructor to perform API calls
        /// </summary>
        /// <param name="api">Api instance</param>
        /// <param name="cache">Icahce instance</param>
        public RatingKeyRepository(IApi<RatingKeyViewModel> api, ICache cache, ILog log)
        {
            Cache = cache;
            Api = api;
            Log = log;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get list of local rating keys
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <param name="language">Passed in language from web UI</param>
        /// <returns>Model of rating keys </returns>
        public async Task<RatingKeyViewModel> GetRatingKeys(string uri, string language)
        {
            //try get cached data first
            var cacheKey = ConstructCacheKey(language);
            var cached = Cache.Get<RatingKeyViewModel>(cacheKey);

            if (cached != null)
            {
                return cached;
            }
            //if no cached data, get data from API and cache it
            try
            {
                var ratingKeys = await Api.GetAsync(uri, language);
                Cache.Add(cacheKey, ratingKeys, DateTime.Now.AddMinutes(30));
                return ratingKeys;
            }
            catch (Exception ex)
            {
                Log.Error("Get rating keys throw unhandled Exception: /r/n", ex);
                throw;
            }
        }

        #endregion

        #region Private Method
        /// <summary>
        /// Create cache key
        /// </summary>
        /// <returns>Cache Key</returns>
        private static string ConstructCacheKey(string language)
        {
            return $"authority_RatingKeys_{language}";
        }
        #endregion
    }
}

