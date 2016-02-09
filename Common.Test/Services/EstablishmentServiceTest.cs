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
    public class EstablishmentServiceTest
    {
        private EstablishmentsViewModel _model = new EstablishmentsViewModel();
        private const string Uri = "Establishments";
        private Dictionary<string, string> _queryString;
        private Dictionary<string, string> _ratingKeyValues;
        [SetUp]
        public void BeforeTests()
        {
            _queryString = new Dictionary<string, string>();
            _ratingKeyValues = new Dictionary<string, string>();
            //provide a dummy rating
            var estab = new List<EstablishmentsModel>();
            var ratings = new[] {"1", "2", "3", "4", "5", "Exempt"};
            for (var i = 0; i <= 5; i++)
            {
                //add 1 rating to each model. so Percentage should be 1/6
                estab.Add(new EstablishmentsModel()
                        {
                            RatingValue = ratings[i]
                        });
                _ratingKeyValues[ratings[i]] = ratings[i];
            }
            _model.Establishments = estab;

        }
        [Test]
        public async Task Repository_Establishments_GetRating_Return_Percentage()
        {
            //Mock Api service
            var estabRepo = new Mock<IApi<EstablishmentsViewModel>>();
            estabRepo.Setup(x => x.GetAsync(Uri, string.Empty)).Returns(Task.FromResult(_model));

            //Mock ICache for testing
            var cacheMock = new Mock<ICache>();
            cacheMock.Setup(x => x.Get<EstablishmentsViewModel>(It.IsAny<string>())).Returns(new EstablishmentsViewModel());
            cacheMock.Setup(x => x.Add(It.IsAny<string>(),It.IsAny<EstablishmentsViewModel>(), It.IsAny<DateTime>())).Verifiable();

            var repo = new EstablishmentRepository(new Mock<ILog>().Object, cacheMock.Object, estabRepo.Object);
            var result = await repo.GetRating(Uri, _queryString, _ratingKeyValues, string.Empty);

            //Enumate to list to aviod null exception
            var resultList = result as IList<RatingViewModel> ?? result.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(_model.Establishments.Count(), resultList.Count());
            Assert.That(resultList.All(x => x.Percentage == (decimal)1 / 6));//make sure percentages are correct
        }
       

    }
}