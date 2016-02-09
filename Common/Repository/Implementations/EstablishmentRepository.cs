using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common.Enums;
using Common.Infrastructure.Api;
using Common.Infrastructure.Cache;
using Common.Repository.Interfaces;
using Common.ViewModel;
using log4net;

namespace Common.Repository.Implementations
{
    /// <summary>
    /// Implementation of IEstablishmentRepository
    /// </summary>
    public class EstablishmentRepository : IEstablishmentRepository
    {
        #region Properties

        /// <summary>
        /// API service property
        /// </summary>
        private IApi<EstablishmentsViewModel> Api { get; }
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
        /// Inject Log4Net and API service in order to perform API calls and activity logging
        /// </summary>
        /// <param name="log">Log4Net</param>
        /// <param name="cache">Cache</param>
        /// <param name="api">Api service</param>
        public EstablishmentRepository(ILog log, ICache cache, IApi<EstablishmentsViewModel> api)
        {
            Api = api;
            Log = log;
            Cache = cache;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Get establishments data and build Rating model
        /// </summary>
        /// <param name="uri">API uri</param>
        /// <param name="queryString">Query string</param>
        /// <param name="RatingKeyValue">Language enabled ratning name</param>
        /// <returns>List of eatablishment rating model</returns>
        public async Task<IEnumerable<RatingViewModel>> GetRating(string uri, Dictionary<string, string> queryString, Dictionary<string,string> RatingKeyValue, string language)
        {
            //Try get cached data first
            var cacheKey = ConstructCacheKey(queryString, language);
            var cached = Cache.Get<EstablishmentsViewModel>(cacheKey);

            var model = cached;
            //if no data cached, get data from Api and cache it
            if (model == null)
            {
                try
                {
                    model = await Api.GetAsync(GetUri(uri, queryString), language);
                    Cache.Add(cacheKey, model, DateTime.Now.AddMinutes(30));
                }
                catch (Exception ex)
                {
                    Log.Error("Get esablishment from API throw unhandled Exception: /r/n", ex);
                    throw;
                }
            }
           //Create rating model
            if (model != null)
            {
                try
                {
                    var restult = model.Establishments.GroupBy(x => x.RatingValue).Select(group => new { RatingName = RatingKeyValue[group.Key], Count = group.Count() }).OrderBy(x => x.RatingName).ToList();

                    return restult.Select(x => new RatingViewModel() { RatingName = x.RatingName, Percentage = (decimal)x.Count / model.Establishments.Count() })
                        .ToList();
                }
                catch (Exception ex)
                {
                    Log.Error("Get establishment rating throw unhandled Exception: /r/n", ex);
                    throw;
                }
                
            }
            Log.Info("Failed to get establishments from remote Api");
            return null;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Build uri for API calls. If query string dictionary provided, append all query string
        /// </summary>
        /// <param name="uri">Api Rui</param>
        /// <param name="queryString">Query string dictionary</param>
        /// <returns>Api uri and all query string for filtering</returns>
        private static string GetUri(string uri, Dictionary<string, string> queryString)
        {
            //if no query string provided, just return base uri
            if (queryString.Count <= 0) return uri;
            var array = queryString.Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}").ToArray();
            return $"{uri}?{string.Join("&", array)}";
        }

        /// <summary>
        /// Create cache key, concatenate all query string value to a string
        /// </summary>
        /// <param name="queryString">Selected authority id</param>
        /// <returns>Cache key</returns>
        private static string ConstructCacheKey(Dictionary<string, string> queryString, string language)
        {
            var qs = string.Join("-", queryString.Select(x => x.Value).ToArray());
            return $"authority_{qs}_{language}";
        }
        #endregion
    }
}