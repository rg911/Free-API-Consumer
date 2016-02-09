using System;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Extensions;
using Common.Model;
using Common.Repository.Interfaces;
using Common.ViewModel;
using NUnit.Framework;
using Moq;
using Web.Controllers;
using Web.Models;

namespace Web.Test
{
    [TestFixture]
    public class IndexControllerTest
    {
        private  AuthoritiesViewModel _authorities = new AuthoritiesViewModel();
        private RatingKeyViewModel _ratingKeyViewModel = new RatingKeyViewModel();
        private  List<RatingViewModel> _ratings = new List<RatingViewModel>();
        private List<RatingKeyModel> _ratingKeys = new List<RatingKeyModel>();

        [SetUp]
        public void BeforeTest()
        {
            //Mock models
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
            _authorities.Authorities = auth;

            //provide a dummy rating
            var ratings = new[] {"1", "2", "3", "4", "5", "Exempt"};
            for (var i = 0; i <= 5; i++)
            {
                //add 1 rating to each model. so Percentage should be 1/6
                _ratings.Add(new RatingViewModel()
                {
                    RatingName = ratings[i],
                    Percentage = ((decimal) 1/ratings.Length).RoundPercentage()
                });
                _ratingKeys.Add(new RatingKeyModel()
                {
                    RatingKey = ratings[i],
                    RatingName = ratings[i]
                });
            }

            _ratingKeyViewModel.RatingKeys = _ratingKeys;

        }

        [Test]
        public async Task Web_Home_Index_Get_Test()
        {
            // Arrange
            var homeController = GetMockHomeController();
            var result = await homeController.Index() as ViewResult;

            Assert.IsNotNull(result.ViewBag.AuthorityList);
            Assert.AreEqual(10, ((SelectList)result.ViewBag.AuthorityList).Count());
        }

        

        [Test]
        public async Task Web_Home_Index_Post_Return_Valid_Model()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController();
            var result = await homeController.Index(model) as ViewResult;
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Selected_Authority()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController();
            var result = await homeController.Index(model) as ViewResult;

            Assert.AreEqual(1, ((ResultsViewModel)result.Model).SelectedAuthorityId);
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Valid_ViewBag()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController();
            var result = await homeController.Index(model) as ViewResult;

            Assert.IsNotNull(result.ViewBag.AuthorityList);
            Assert.AreEqual(10, ((SelectList)result.ViewBag.AuthorityList).Count());
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Correct_RecordCount()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController();
            var result = await homeController.Index(model) as ViewResult;

            Assert.AreEqual(6, ((ResultsViewModel)result.Model).Ratings.Count());
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Correct_Percentage()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController();
            var result = await homeController.Index(model) as ViewResult;

            Assert.AreEqual(((decimal)1 / 6).RoundPercentage(), ((ResultsViewModel)result.Model).Ratings.Select(x => x.Percentage).FirstOrDefault());
        }

        [Test]
        public async Task Web_Home_Index_English_Language_Return_Default_RatingName()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController();
            var result = await homeController.Index(model) as ViewResult;

            Assert.AreEqual(((decimal)1 / 6).RoundPercentage(), ((ResultsViewModel)result.Model).Ratings.Select(x => x.Percentage).FirstOrDefault());

            //Make sure that the controller is still working
            Assert.IsNotNull(result.ViewBag.AuthorityList);
            Assert.AreEqual(10, ((SelectList)result.ViewBag.AuthorityList).Count());
            Assert.AreEqual(6, ((ResultsViewModel)result.Model).Ratings.Count());
            Assert.AreEqual(((decimal)1 / 6).RoundPercentage(), ((ResultsViewModel)result.Model).Ratings.Select(x => x.Percentage).FirstOrDefault());
            //Language
            Assert.That(model.Ratings.All(x => !x.RatingName.Contains("WELSH")));
        }

        [Test]
        public async Task Web_Home_Index_Welsh_Language_Return_Language_Content()
        {
            var model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = GetMockHomeController("WELSH");
            var result = await homeController.Index(model) as ViewResult;

            //Make sure that the controller is still working
            Assert.IsNotNull(result.ViewBag.AuthorityList);
            Assert.AreEqual(10, ((SelectList)result.ViewBag.AuthorityList).Count());
            Assert.AreEqual(6, ((ResultsViewModel)result.Model).Ratings.Count());
            Assert.AreEqual(((decimal)1 / 6).RoundPercentage(), ((ResultsViewModel)result.Model).Ratings.Select(x => x.Percentage).FirstOrDefault());
            //Language
            Assert.That(model.Ratings.All(x => x.RatingName.Contains("WELSH")));
            
        }


        private HomeController GetMockHomeController(string lang = "English")
        {
            //Setup Mock objects
            var authorityRepository = new Mock<IAuthorityRepository>();
            var establishmentRepository = new Mock<IEstablishmentRepository>();
            var ratingKeyRepository = new Mock<IRatingKeyRepository>();
            var httpContextMock = new Mock<HttpContextBase>(MockBehavior.Loose);
            var controllerMock = new Mock<ControllerBase>(MockBehavior.Loose);
            var routeData = new RouteData();
            //Mock model in different language
            if (!lang.Equals("English"))
            {
                _ratings = _ratings.Select(x =>
                {
                    x.RatingName = $"{x.RatingName}_WELSH";
                    return x;
                }).ToList();
            }

            //Mock 
            authorityRepository.Setup(x => x.GetAuthorities(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_authorities));
            ratingKeyRepository.Setup(x => x.GetRatingKeys(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_ratingKeyViewModel));

            establishmentRepository.Setup(x => x.GetRating(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<Dictionary<string, string>>(),It.IsAny<string>())).Returns(Task.FromResult((IEnumerable<RatingViewModel>)_ratings));
            routeData.Values.Add("language", lang);

            

            var controllerContext = new ControllerContext(httpContextMock.Object, routeData, controllerMock.Object);

            var homeController = new HomeController(authorityRepository.Object, establishmentRepository.Object,
                ratingKeyRepository.Object)
            { ControllerContext = controllerContext };
            return homeController;
        }

        [TearDown]
        public void AfterTest()
        {
            _authorities = new AuthoritiesViewModel();
            _ratingKeyViewModel = new RatingKeyViewModel();
            _ratings = new List<RatingViewModel>();
            _ratingKeys = new List<RatingKeyModel>();
        }
    }
}