using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Common.Repository.Interfaces;
using Common.WebConfig;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        #region Dependencies
        private IRatingKeyRepository RatingKeyRepository { get; }
        private IAuthorityRepository AuthorityRepository { get; }
        private IEstablishmentRepository EstablishmentRepository { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor with service dependencies injected
        /// </summary>
        /// <param name="authorityRepository">Authority Service</param>
        /// <param name="establishmentRepository">Establishment Service</param>
        public HomeController(IAuthorityRepository authorityRepository, IEstablishmentRepository establishmentRepository, IRatingKeyRepository ratingKeyRepository)
        {
            AuthorityRepository = authorityRepository;
            EstablishmentRepository = establishmentRepository;
            RatingKeyRepository = ratingKeyRepository;
        }
        #endregion

        /// <summary>
        /// Controler for home page. On http get, we get list of authorities to draw the dropdownlist.
        /// Authority list is cached on first call
        /// </summary>
        /// <returns>Action result of Index controller</returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //First get language from route
            var language = ControllerContext.RouteData.Values["language"].ToString();
            //Get list of local authorities
            var authorities = await AuthorityRepository.GetAuthorities(ApiConstant.AuthorityUri, language);
           

            ViewBag.AuthorityList = new SelectList(authorities.Authorities.OrderBy(x => x.RegionName).ThenBy(x => x.Name), "Id", "Name", "RegionName", 0);
            ViewBag.Language = language ?? "English";
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
        public async Task<ActionResult> Index([Bind(Include = "SelectedAuthorityId")] ResultsViewModel model)
        {
            var language = ControllerContext.RouteData.Values["language"].ToString();

            //escape default select 'select authority' option
            if (model.SelectedAuthorityId != 0)
            {
                //Get language enabled rating names
                var ratingKeys = await RatingKeyRepository.GetRatingKeys(ApiConstant.RatingsUri, language);
                //Get establishment API query string
                var queryString = new Dictionary<string, string>
                {
                    {ApiConstant.QueryStringKeyLocalAuthority, model.SelectedAuthorityId.ToString()}
                };
                //Get establishment
                model.Ratings = await EstablishmentRepository.GetRating(ApiConstant.EstablishmentUri, queryString, ratingKeys.RatingKeys.ToDictionary(x=>x.RatingKey,x=>x.RatingName), language);
            }

            var authorities = await AuthorityRepository.GetAuthorities(ApiConstant.AuthorityUri, language);
            ViewBag.AuthorityList =
                new SelectList(authorities.Authorities.OrderBy(x => x.RegionName).ThenBy(x => x.Name), "Id", "Name",
                    "RegionName", model.SelectedAuthorityId);
            ViewBag.Language = language ?? "English";
            return View(model);
        }


    }
}