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

        //API Header
        public const string HeaderRequestKey = "x-api-version";
        public const string HeaderRequestValue = "2";

        //Query String
        public const string QueryStringKeyLocalAuthority = "localAuthorityId";
    }
}
