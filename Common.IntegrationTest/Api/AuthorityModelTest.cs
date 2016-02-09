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
    public class AuthorityModelTest
    {

        [Test]
        public async Task Service_Authority_Single_Return_One_Record()
        {
            var api = new Api<AuthorityModel>(new Mock<ILog>().Object);
            var result = await api.GetAsync("Authorities/197", string.Empty);
            Assert.IsNotNull(result);
            Assert.That(result.Id == 197);
            Assert.That(!string.IsNullOrEmpty(result.Name));
            Assert.That(!string.IsNullOrEmpty(result.RegionName));
        }

        [Test]
        public async Task Service_Authority_AllAuthorities_Returns_All()
        {
            var api = new Api<AuthoritiesViewModel>(new Mock<ILog>().Object);
            var result = await api.GetAsync("Authorities", string.Empty);
            Assert.IsNotNull(result);
            Assert.That(result.Authorities.Any());
        }
       
    }
}