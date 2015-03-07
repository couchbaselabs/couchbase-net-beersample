using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Couchbase.BeerSample.Domain.Misc
{
    public class IgnoreFieldContractResolver : DefaultContractResolver
    {
        public IgnoreFieldContractResolver()
        {
        }

        public IgnoreFieldContractResolver(string fieldToIgnore)
        {
            FieldToIgnore = fieldToIgnore;
        }

        public string FieldToIgnore { get; set; }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (string.IsNullOrWhiteSpace(FieldToIgnore))
            {
                throw new InvalidOperationException("FieldToIgnore must be non-null and not-empty.");
            }
            return (from x in base.CreateProperties(type, memberSerialization)
                    where x.PropertyName != FieldToIgnore
                    select x).ToList();
        }
    }
}
