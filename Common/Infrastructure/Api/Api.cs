using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using Common.WebConfig;

namespace Common.Infrastructure.Api
{
    /// <summary>
    /// Implementation of IAPI interface
    /// </summary>
    /// <typeparam name="T">Generic class for Json models</typeparam>
    public class Api<T> :ApiBase, IApi<T> where T: class
    {
        public string BaseApiUrl { get; set; }

        #region Constructor
        /// <summary>
        /// Taking a new instance of HttpClient and set it up in base class
        /// </summary>
        /// <param name="log">Log4Net injected</param>
        public Api(ILog log) : base(log)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generic method to performa a single API Get call to build up content model.
        /// Can handle differnt API endpoint calls and generate strong typed data model.
        /// </summary>
        /// <param name="uri">Web api uri</param>
        /// <returns>Api get content (Json deserialized data model)</returns>
        public async Task<T> GetAsync(string uri)
        {
            using (var client = HttpClientSetup(BaseApiUrl))
            {
                
                T result = null;
                try
                {
                    //Send http request call the API GET
                    //Should throw fatal exception if bad http url passed
                    var response = await client.GetAsync(uri).ConfigureAwait(false);
                    //Get the http response and throw exception if response status is not successful
                    response.EnsureSuccessStatusCode();
                    //Read response data into Json format
                    await response.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        if (x.IsFaulted)
                        {
                            if (x.Exception != null)
                            {
                                Log.Error("GetAsync Task Exception: /r/n", x.Exception);
                                throw x.Exception;
                            }
                        }
                        //Build generic model 
                        result = JsonConvert.DeserializeObject<T>(x.Result);

                        AuditLog(BaseApiUrl, uri, response.StatusCode.ToString(), response.RequestMessage.ToString(),
                            response.IsSuccessStatusCode);
                    });
                }
                catch (HttpRequestException ex) //handle http request exception
                {
                    Log.Error("GetAsync Request Http Exception: /r/n", ex);
                    throw;
                }
                catch (Exception ex) //unhandled exception
                {
                    Log.Error("GetAsync Request Unhandled Exception: /r/n", ex);
                    throw;
                }
                return result;
            }
        }
        #endregion

        #region Private Method

        private static HttpClient HttpClientSetup(string baseApiUrl)
        {
            HttpClient client = new HttpClient {BaseAddress = new Uri(baseApiUrl ?? ApiConstant.BassApiUrl)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(ApiConstant.HeaderRequestKey, ApiConstant.HeaderRequestValue);
            return client;
        }
        #endregion

    }
}
