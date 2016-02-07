using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.ViewModel;

namespace Common.Services.Interfaces
{
    /// <summary>
    /// Authority services handler
    /// </summary>
    public interface IAuthorityService
    {
        /// <summary>
        /// Call API get all local authrities
        /// </summary>
        /// <param name="uri">API Uri</param>
        /// <returns>Closed constructed local authority model</returns>
        Task<AuthoritiesViewModel> GetAuthorities(string uri);
    }
}
