using System.Threading.Tasks;
using Common.Enums;
using Common.ViewModel;

namespace Common.Repository.Interfaces
{
    /// <summary>
    /// Authority services handler
    /// </summary>
    public interface IAuthorityRepository
    {
        /// <summary>
        /// Call API get all local authrities
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <param name="language">Language</param>
        /// <returns>Closed constructed local authority model</returns>
        Task<AuthoritiesViewModel> GetAuthorities(string uri, string language);
    }
}
