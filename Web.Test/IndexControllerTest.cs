using System;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Extensions;
using Common.Model;
using Common.Services.Interfaces;
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
        private readonly AuthoritiesViewModel _authorities = new AuthoritiesViewModel();
        private readonly List<RatingViewModel> _ratings = new List<RatingViewModel>();

        [SetUp]
        public void BeforeTest()
        {
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
            var ratings = new[] { "1", "2", "3", "4", "5", "Exempt" };
            for (var i = 0; i <= 5; i++)
            {
                //add 1 rating to each model. so Percentage should be 1/6
                _ratings.Add(new RatingViewModel()
                {
                    RatingName = ratings[i],
                    Percentage = ((decimal)1/ratings.Length).RoundPercentage()
                });
            }
        }

        [Test]
        public async Task Web_Home_Index_Get_Test()
        {
            var authorityService = new Mock<IAuthorityService>();
            var establishmentService = new Mock<IEstablishmentService>();

            authorityService.Setup(x => x.GetAuthorities(It.IsAny<string>())).Returns(Task.FromResult(_authorities));

            var homeController = new HomeController(authorityService.Object, establishmentService.Object);
            var result = await homeController.Index() as ViewResult;

            Assert.IsNotNull(result.ViewBag.AuthorityList);
            Assert.AreEqual(10,((SelectList)result.ViewBag.AuthorityList).Count());
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Valid_Model()
        {
            var authorityService = new Mock<IAuthorityService>();
            var establishmentService = new Mock<IEstablishmentService>();

            authorityService.Setup(x => x.GetAuthorities(It.IsAny<string>())).Returns(Task.FromResult(_authorities));
            establishmentService.Setup(x => x.GetRating(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult((IEnumerable<RatingViewModel>)_ratings));

            ResultsViewModel model = new ResultsViewModel() {SelectedAuthorityId = 1};

            var homeController = new HomeController(authorityService.Object, establishmentService.Object);
            var result = await homeController.Index(model) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Selected_Authority()
        {
            var authorityService = new Mock<IAuthorityService>();
            var establishmentService = new Mock<IEstablishmentService>();

            authorityService.Setup(x => x.GetAuthorities(It.IsAny<string>())).Returns(Task.FromResult(_authorities));
            establishmentService.Setup(x => x.GetRating(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult((IEnumerable<RatingViewModel>)_ratings));

            ResultsViewModel model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = new HomeController(authorityService.Object, establishmentService.Object);
            var result = await homeController.Index(model) as ViewResult;

            Assert.AreEqual(1, ((ResultsViewModel)result.Model).SelectedAuthorityId);
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Valid_ViewBag()
        {
            var authorityService = new Mock<IAuthorityService>();
            var establishmentService = new Mock<IEstablishmentService>();

            authorityService.Setup(x => x.GetAuthorities(It.IsAny<string>())).Returns(Task.FromResult(_authorities));
            establishmentService.Setup(x => x.GetRating(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult((IEnumerable<RatingViewModel>)_ratings));

            ResultsViewModel model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = new HomeController(authorityService.Object, establishmentService.Object);
            var result = await homeController.Index(model) as ViewResult;

            Assert.IsNotNull(result.ViewBag.AuthorityList);
            Assert.AreEqual(10, ((SelectList)result.ViewBag.AuthorityList).Count());
        }

        [Test]
        public async Task Web_Home_Index_Post_Return_Correct_Percentage()
        {
            var authorityService = new Mock<IAuthorityService>();
            var establishmentService = new Mock<IEstablishmentService>();

            authorityService.Setup(x => x.GetAuthorities(It.IsAny<string>())).Returns(Task.FromResult(_authorities));
            establishmentService.Setup(x => x.GetRating(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult((IEnumerable<RatingViewModel>)_ratings));

            ResultsViewModel model = new ResultsViewModel() { SelectedAuthorityId = 1 };

            var homeController = new HomeController(authorityService.Object, establishmentService.Object);
            var result = await homeController.Index(model) as ViewResult;

            Assert.AreEqual(((decimal)1 / 6).RoundPercentage(), ((ResultsViewModel)result.Model).Ratings.Select(x => x.Percentage).FirstOrDefault());
        }
    }
}