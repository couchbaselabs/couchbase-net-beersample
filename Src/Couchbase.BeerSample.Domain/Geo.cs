using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Domain
{
    public class Geo
    {
        public string Accuracy { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }
        /*
         * "geo": {
            "accuracy": "ROOFTOP",
            "lat": 37.7825,
            "lon": -122.393
         * */
    }
}
