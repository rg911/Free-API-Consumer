using System.Collections.Generic;
using Common.Model;
using Newtonsoft.Json;

namespace Common.ViewModel
{
    /// <summary>
    /// View model for all local authorities
    /// </summary>
    public class AuthoritiesViewModel
    {
        [JsonProperty("authorities")]
        public IEnumerable<AuthorityModel> Authorities { get; set; }
    }
}
