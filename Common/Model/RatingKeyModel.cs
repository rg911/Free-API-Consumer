using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Model
{
    public class RatingKeyModel
    {
        [JsonProperty("ratingKeyName")]
        public string RatingKey { get; set; }
        [JsonProperty("ratingName")]
        public string RatingName { get; set; }
    }
}
