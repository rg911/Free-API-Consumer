using System.Threading.Tasks;
using Common.Enums;
using Common.ViewModel;

namespace Common.Repository.Interfaces
{
    /// <summary>
    /// Get rating keys
    /// </summary>
    public interface IRatingKeyRepository
    {
        /// <summary>
        /// Call API get all rating keys
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <param name="language">Language</param>
        /// <returns>Closed constructed local authority model</returns>
        Task<RatingKeyViewModel> GetRatingKeys(string uri, string language);
    }
}
