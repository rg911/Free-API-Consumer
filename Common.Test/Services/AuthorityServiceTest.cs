using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Infrastructure.Api;
using Common.Infrastructure.Cache;
using Common.Model;
using Common.Repository.Implementations;
using Common.ViewModel;
using log4net;
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
        public async Task Repository_Authority_Get_Return_Authority()
        {
            //Mock Api service
            var api = new Mock<IApi<AuthoritiesViewModel>>();
            api.Setup(x => x.GetAsync(string.Empty, string.Empty)).Returns(Task.FromResult(_model));
            //Mock ICache for testing
            var cacheMock = new Mock<ICache>();
            cacheMock.Setup(x => x.Get<EstablishmentsViewModel>(It.IsAny<string>())).Returns(new EstablishmentsViewModel());
            cacheMock.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<EstablishmentsViewModel>(), It.IsAny<DateTime>())).Verifiable();


            var authorityService = new AuthorityRepository(api.Object, cacheMock.Object, new Mock<ILog>().Object);
            var result = await authorityService.GetAuthorities(string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Authorities.Count());
        }
    }
}