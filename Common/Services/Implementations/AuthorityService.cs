using System.Threading.Tasks;
using Common.Infrastructure.Api;
using Common.Services.Interfaces;
using Common.ViewModel;
using log4net;

namespace Common.Services.Implementations
{
    public class AuthorityService : IAuthorityService
    {
        #region Properties

        private IApi<AuthoritiesViewModel> Api { get; } 
        #endregion

        #region Constructor

        /// <summary>
        /// Taking a new instance of HttpClient and set it up in base class
        /// </summary>
        /// <param name="api">Api instance</param>
        public AuthorityService(IApi<AuthoritiesViewModel> api ) 
        {
            Api = api;
        }
        #endregion

        #region Public Method

        public async Task<AuthoritiesViewModel> GetAuthorities(string uri)
        {
            return await Api.GetAsync(uri);
        }

        #endregion
    }
}
