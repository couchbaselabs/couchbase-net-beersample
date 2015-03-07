using Couchbase.BeerSample.Domain.Persistence.Core;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Domain
{
    public class Beer : EntityBase
    {
        public string Name { get; set; }

        public decimal Abv { get; set; }

        public decimal Ibu { get; set; }

        public decimal Srm { get; set; }

        public decimal Upc { get; set; }

        [JsonProperty("brewery_id")]
        public string BreweryId { get; set; }

        public string Description { get; set; }

        public string Style { get; set; }

        public string Category { get; set; }
    }
}
