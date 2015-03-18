
using System;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Domain.Persistence.Core
{
    public abstract class EntityBase : IEntity
    {
        private static string _typeName;

        protected EntityBase()
        {
            if (_typeName == null)
            {
                _typeName = GetType().Name;
            }
            Type = _typeName;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        public string Type { get; set; }

        public ulong Cas { get; set; }

        public DateTime Updated { get; set; }
    }
}
