using Couchbase.BeerSample.Domain.Persistence;
using Couchbase.BeerSample.Domain.Persistence.Core;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Domain
{
    public class Geo : EntityBase
    {
        public string Accuracy { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }
    }
}
