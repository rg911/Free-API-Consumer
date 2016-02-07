using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Infrastructure.Api;
using Common.Model;
using Common.Services.Implementations;
using Common.ViewModel;
using Moq;
using NUnit.Framework;

namespace Common.UnitTest.Services
{
    [TestFixture]
    public class AuthorityServiceTest
    {
        private readonly AuthoritiesViewModel _model = new AuthoritiesViewModel();
        [SetUp]
        public void BeforeTests()
        {
            //Mock authority model
            var auth = new List<AuthorityModel>();
            for (var i = 1; i <= 10; i++)
            {
                auth.Add(new AuthorityModel()
                        {
                            Name = $"Authority {i}",
                            Id = i,
                            RegionName = $"Region {i}"
                        });
            }
            _model.Authorities = auth;
        }
        [Test]
        public async Task Service_Authority_Get_Return_Authority()
        {
            //Mock Api service
            var api = new Mock<IApi<AuthoritiesViewModel>>();
            api.Setup(x => x.GetAsync(string.Empty)).Returns(Task.FromResult(_model));

            var authorityService = new AuthorityService(api.Object);
            var result = await authorityService.GetAuthorities(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Authorities.Count());
        }
    }
}