using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infrastructure.Api
{
    /// <summary>
    /// This is an open generic API handler to perform API GET calls. It can be implemented by passing 
    /// any models taking returned Json results. The implementation of this interface is also open generic. 
    /// However it is injected to individual services (e.g. Authority services and establishment services) 
    /// which used closed construced type (strong type)
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
