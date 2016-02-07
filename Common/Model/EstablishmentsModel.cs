using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Model
{
    public class EstablishmentsModel
    {
        [JsonProperty("RatingValue")]
        public string RatingValue { get; set; }
    }
}
