
using System;
using System.Collections.Generic;
using System.Linq;
using Couchbase.BeerSample.Domain;

namespace Couchbase.BeerSample.Web.Models
{
    public class BreweryViewModel
    {
        public BreweryViewModel()
        {
        }

        public BreweryViewModel(Brewery brewery)
        {
            Name = brewery.Name;
            City = brewery.City;
            State = brewery.State;
            Code = brewery.Code;
            Country = brewery.Country;
            Phone = brewery.Phone;
            Website = brewery.Website;
            Description = brewery.Description;
            Address = brewery.Address == null ? "" : brewery.Address.FirstOrDefault();
            Geo = brewery.Geo ?? new Geo();
        }
        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Code { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Geo Geo { get; set; }

        public void Update(Brewery brewery)
        {
            brewery.Name = Name;
            brewery.Geo = Geo;
            brewery.Phone = Phone;
            brewery.State = State;
            brewery.Code = Code;
            brewery.City = City;
            brewery.Website = Website;
            brewery.Description = Description;
            brewery.Country = Country;
            brewery.Updated = DateTime.Now;

            if (brewery.Address == null)
            {
                brewery.Address = new List<string>
                {
                    Address
                };
            }
            else
            {
                if (brewery.Address.Any())
                {
                    brewery.Address[0] = Address;
                }
                else
                {
                    brewery.Address.Add(Address);
                }
            }
        }
    }
}