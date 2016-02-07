using System.Threading.Tasks;
using Common.Infrastructure.Api;
using Common.Services.Interfaces;
using Common.ViewModel;
using log4net;

namespace Common.Services.Implementations
{
    /// <summary>
    /// Implementation of IAuthorityService
    /// </summary>
    public class AuthorityService : IAuthorityService
    {
        #region Properties
        /// <summary>
        /// Local API service property
        /// </summary>
        private IApi<AuthoritiesViewModel> Api { get; } 
        #endregion

        #region Constructor

        /// <summary>
        /// Inject API dependency to constructor to perform API calls
        /// </summary>
        /// <param name="api">Api instance</param>
        public AuthorityService(IApi<AuthoritiesViewModel> api ) 
        {
            Api = api;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get list of local authorities
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <returns>Model of local authority which contains list of Local Authorities</returns>
        public async Task<AuthoritiesViewModel> GetAuthorities(string uri)
        {
            return await Api.GetAsync(uri);
        }

        #endregion
    }
}
