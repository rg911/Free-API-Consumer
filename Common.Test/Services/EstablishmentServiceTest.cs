using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Infrastructure.Api;
using Common.Model;
using Common.Services.Implementations;
using Common.ViewModel;
using log4net;
using Moq;
using NUnit.Framework;

namespace Common.UnitTest.Services
{
    [TestFixture]
    public class EstablishmentServiceTest
    {
        private readonly EstablishmentsViewModel _model = new EstablishmentsViewModel();
        private const string Uri = "Establishments";
        private Dictionary<string, string> _queryString;
        [SetUp]
        public void BeforeTests()
        {
            _queryString = new Dictionary<string, string>();
            //mock establishment model
            var establishemt = new List<EstablishmentsModel>();
            //provide a dummy rating
            var ratings = new[] {"1", "2", "3", "4", "5", "Exempt"};
            for (var i = 0; i <= 5; i++)
            {
                //add 1 rating to each model. so Percentage should be 1/6
                establishemt.Add(new EstablishmentsModel()
                        {
                            RatingValue = ratings[i]
                        });
            }
            _model.Establishments = establishemt;
        }
        [Test]
        public async Task Service_Establishments_GetRating_Return_Percentage()
        {
            //Mock Api service
            var api = new Mock<IApi<EstablishmentsViewModel>>();
            api.Setup(x => x.GetAsync(Uri)).Returns(Task.FromResult(_model));

            var service = new EstablishmentService(new Mock<ILog>().Object,api.Object);
            var result = await service.GetRating(Uri, _queryString);

            //Enumate to list to aviod null exception
            var resultList = result as IList<RatingViewModel> ?? result.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(_model.Establishments.Count(), resultList.Count());
            Assert.That(resultList.All(x => x.Percentage == (decimal)1 / 6));//make sure percentages are correct
        }
       

    }
}