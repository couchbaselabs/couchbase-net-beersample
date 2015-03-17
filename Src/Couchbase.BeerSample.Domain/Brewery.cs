
using System.Collections.Generic;
using Couchbase.BeerSample.Domain.Persistence;
using Couchbase.BeerSample.Domain.Persistence.Core;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Domain
{
    public class Brewery : EntityBase
    {
        public Brewery()
        {
            Geo = new Geo();
            Address = new List<string>();
        }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Code { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Description { get; set; }

        public List<string> Address { get; set; }

        public Geo Geo { get; set; }
    }
}
