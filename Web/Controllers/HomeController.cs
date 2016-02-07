using System.Collections.Generic;
using System.Web.Mvc;
using Common.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.UI;
using Common.WebConfig;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        #region Dependencies

        private IAuthorityService AuthorityService { get; }
        private IEstablishmentService EstablishmentService { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor with service dependencies injected
        /// </summary>
        /// <param name="authorityService">Authority Service</param>
        /// <param name="establishmentService">Establishment Service</param>
        public HomeController(IAuthorityService authorityService, IEstablishmentService establishmentService)
        {
            AuthorityService = authorityService;
            EstablishmentService = establishmentService;
        }
        #endregion

        /// <summary>
        /// Controler for home page. On http get, we get list of authorities to draw the dropdownlist.
        /// Authority list is cached on first call
        /// </summary>
        /// <returns>Action result of Index controller</returns>
        [HttpGet]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public async Task<ActionResult> Index()
        {
            //Get list of local authorities
            var authorities = await AuthorityService.GetAuthorities(ApiConstant.AuthorityUri);
            //Create selectlist for dropdown
            ViewBag.AuthorityList = new SelectList(authorities.Authorities, "Id", "Name", "RegionName",-1);
            return View(new ResultsViewModel { Ratings = null });
        }

        /// <summary>
        /// Index post controller, return rating list by user authority selection
        /// We cached the json results by 10 mins the api data is not a realtime transactional data
        /// </summary>
        /// <param name="model">Rating model</param>
        /// <returns>Rating of selected authority</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "SelectedAuthorityId")]
        public async Task<ActionResult> Index([Bind(Include = "SelectedAuthorityId")] ResultsViewModel model)
        {
            //Get establishment API query string
            var queryString = new Dictionary<string, string>
            {
                {ApiConstant.QueryStringKeyLocalAuthority, model.SelectedAuthorityId.ToString()}
            };
            //Get establishment
            model.Ratings = await EstablishmentService.GetRating(ApiConstant.EstablishmentUri, queryString);

            var authorities = await AuthorityService.GetAuthorities(ApiConstant.AuthorityUri);
            ViewBag.AuthorityList = new SelectList(authorities.Authorities, "Id", "Name", "RegionName",model.SelectedAuthorityId);
            return View(model);
        }

    }
}