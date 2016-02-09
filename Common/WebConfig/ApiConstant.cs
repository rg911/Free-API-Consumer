using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebConfig
{
    /// <summary>
    /// Holds common web config settings
    /// </summary>
    public static class ApiConstant
    {
        public const string BassApiUrl = "http://api.ratings.food.gov.uk/";
        public const string AuthorityUri = "Authorities";
        public const string EstablishmentUri = "Establishments";
        public const string RatingsUri = "Ratings";

        //API Header
        public const string HeaderRequestKeyVersion = "x-api-version";
        public const string HeaderRequestValueVerion = "2";

        public const string HeaderRequestKeyLanguageWelsh = "Accept-Language";
        public const string HeaderRequestValueLanguageWelsh = "cy-GB";

        //Query String
        public const string QueryStringKeyLocalAuthority = "localAuthorityId";
    }
}
