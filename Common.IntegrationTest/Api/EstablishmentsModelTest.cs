using System.Linq;
using System.Threading.Tasks;
using Common.Infrastructure.Api;
using Common.Model;
using Common.ViewModel;
using log4net;
using Moq;
using NUnit.Framework;

namespace Common.IntegrationTest.Api
{
    [TestFixture]
    public class EstablishmentsModelTest
    {
        [Test]
        public async Task Service_Establishments_Single_Return_One_Record()
        {
            var api = new Api<EstablishmentsModel>(new Mock<ILog>().Object);
            var result = await api.GetAsync("Establishments/1", string.Empty);
            Assert.IsNotNull(result);
            Assert.That(!string.IsNullOrEmpty(result.RatingValue));
        }

        [Test]
        public async Task Service_Establishments_AllAuthorities_Returns_All()
        {
            var api = new Api<EstablishmentsViewModel>(new Mock<ILog>().Object);
            var result = await api.GetAsync("Establishments?LocalAuthorityId=197", string.Empty);
            Assert.IsNotNull(result);
            Assert.That(result.Establishments.Any());
        }

        [Test]
        public async Task Service_Establishments_GetRating_Returns_All()
        {
            var apiService = new Api<EstablishmentsViewModel>(new Mock<ILog>().Object);
            var model = await apiService.GetAsync("Establishments?LocalAuthorityId=197", string.Empty);
            var restultEs = model.Establishments.GroupBy(x => x.RatingValue).Select(group=> new { RateValue = group.Key, Count = group.Count() }).OrderBy(x=>x.RateValue).ToList();
            var result = restultEs.Select(x => new RatingViewModel() { RatingName = x.RateValue, Percentage = (decimal)x.Count / model.Establishments.Count() })
                 .ToList();
            Assert.IsNotNull(result);
            Assert.That(result.Any());
            Assert.IsNotNull(result.Select(x=>x.RatingName).FirstOrDefault());
            Assert.IsNotNull(result.Select(x => x.Percentage).FirstOrDefault());
        }

        
    }
}