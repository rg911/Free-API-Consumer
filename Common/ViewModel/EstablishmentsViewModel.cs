using System.Collections.Generic;
using Common.Model;
using Newtonsoft.Json;
namespace Common.ViewModel
{
    public class EstablishmentsViewModel
    {
        [JsonProperty("establishments")]
        public IEnumerable<EstablishmentsModel> Establishments { get; set; }
        
    }
}
