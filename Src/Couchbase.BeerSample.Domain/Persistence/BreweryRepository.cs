
using System.Collections.Generic;
using Couchbase.BeerSample.Domain.Persistence.Core;
using Couchbase.Core;
using Couchbase.N1QL;

namespace Couchbase.BeerSample.Domain.Persistence
{
    public class BreweryRepository : Repository<Brewery>
    {
        public BreweryRepository(IBucket bucket)
            : base(bucket)
        {
        }

        public IEnumerable<Brewery> SelectAllBreweries(int index, int limit)
        {
            const string statement = "SELECT b.*, META(b).id FROM `beer-sample` as b " +
                                     "ORDER BY name DESC " +
                                     "LIMIT $1 " +
                                     "OFFSET $2;";

            return Select(new QueryRequest(statement)
                .AddPositionalParameter(limit)
                .AddPositionalParameter(index));
        }
    }
}