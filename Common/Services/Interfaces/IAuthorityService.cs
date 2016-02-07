using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.ViewModel;

namespace Common.Services.Interfaces
{
    public interface IAuthorityService
    {
        Task<AuthoritiesViewModel> GetAuthorities(string uri);
    }
}
