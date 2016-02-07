using System;
using log4net;

namespace Common.Infrastructure.Api
{
    /// <summary>
    /// Base service class handle activity logging
    /// </summary>
    public class ApiBase
    {
        protected ILog Log { get; }

        protected ApiBase(ILog log)
        {
            Log = log;
        }

        /// <summary>
        /// Activity audit log
        /// </summary>
        /// <param name="url">Request base url</param>
        /// <param name="uri">Request Uri (Query String)</param>
        /// <param name="httpResponseCode">Http response code</param>
        /// <param name="httpResponseMessage">Http Response message</param>
        /// <param name="isSucceeded">Is http request suceeded</param>
        protected void AuditLog(string url, string uri, string httpResponseCode, string httpResponseMessage, bool isSucceeded)
        {
            var success = isSucceeded ? "suceeded" : "faled";
            Log.InfoFormat($"API: {url+uri} called at {DateTime.Now} {success}. HttpResponseMessage: {httpResponseMessage}({httpResponseCode}) ");
          
        }
    }

   
}
