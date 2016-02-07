using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ViewModel;

namespace Common.Services.Interfaces
{
    /// <summary>
    /// Establishment handler
    /// </summary>
    public interface IEstablishmentService
    {
        /// <summary>
        /// Get list of ratings from API calls
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <param name="queryString">Query string used in API for filtering</param>
        /// <returns>List of rating for selection</returns>
        Task<IEnumerable<RatingViewModel>> GetRating(string uri, Dictionary<string, string> queryString );
    }
}
