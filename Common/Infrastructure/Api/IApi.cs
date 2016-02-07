using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Api
{
    /// <summary>
    /// Generic API handler
    /// </summary>
    /// <typeparam name="T">Model classes</typeparam>
    public interface IApi<T> where T: class
    {   
        string BaseApiUrl { get; set; }
        /// <summary>
        /// Generic method to performa a single API Get call to build up content model
        /// </summary>
        /// <param name="uri">Web api uri</param>
        /// <returns>Api get content (Json deserialized data model)</returns>
        Task<T> GetAsync(string uri);
    }
}
