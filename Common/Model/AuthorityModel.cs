using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Model
{
    public class AuthorityModel
    {
        [JsonProperty("LocalAuthorityId")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("RegionName")]
        public string RegionName { get; set; }

    }
}
