using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ViewModel;

namespace Common.Services.Interfaces
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<RatingViewModel>> GetRating(string uri, Dictionary<string, string> queryString );
    }
}
