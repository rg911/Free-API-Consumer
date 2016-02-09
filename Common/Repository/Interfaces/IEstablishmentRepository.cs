using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ViewModel;

namespace Common.Repository.Interfaces
{
    /// <summary>
    /// Establishment handler
    /// </summary>
    public interface IEstablishmentRepository
    {
        /// <summary>
        /// Get list of ratings from API calls
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <param name="queryString">Query string used in API for filtering</param>
        /// <param name="RatingKeyValue">Language enabled rating names</param>
        /// <param name="language">Language</param>
        /// <returns>List of rating for selection</returns>
        Task<IEnumerable<RatingViewModel>> GetRating(string uri, Dictionary<string, string> queryString , Dictionary<string, string> RatingKeyValue,string language);
    }
}
