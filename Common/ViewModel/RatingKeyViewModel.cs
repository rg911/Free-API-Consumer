using System.Collections.Generic;
using Common.Model;
using Newtonsoft.Json;

namespace Common.ViewModel
{
    public class RatingKeyViewModel
    {
        [JsonProperty("ratings")]
        public IEnumerable<RatingKeyModel> RatingKeys { get; set; }
    }
}
