using System.Collections.Generic;
using System.Data;
using Couchbase.BeerSample.Domain;
using Couchbase.BeerSample.Domain.Exceptions;
using Couchbase.BeerSample.Domain.Persistence.Core;
using Couchbase.Core;
using Couchbase.N1QL;
using Couchbase.Views;

namespace Couchbase.BeerSample.Domain.Persistence
{
    public class BeerRepository : Repository<Beer>
    {
        public BeerRepository(IBucket bucket)
            : base(bucket)
        {
        }

        public IEnumerable<ViewRow<Beer>> SelectBeers(int index, int limit)
        {
            var query = Bucket.CreateQuery("beer", "all_beers")
                .Skip(index)
                .Limit(limit);

            var results = Bucket.Query<Beer>(query);
            if (!results.Success)
            {
                var message = results.Error;
                throw new ViewRequestException(message, results.StatusCode);
            }
            return results.Rows;
        }

        public IEnumerable<Beer> SelectBeerByBrewery(string brewery, int index, int limit)
        {
            const string statement = "SELECT * FROM `beer-sample`"
                                     + " WHERE type = 'beer'"
                                     + " AND brewery_id = $brewery_id"
                                     + " LIMIT $limit"
                                     + " OFFSET $offset";


            return Select(new QueryRequest(statement)
                .AddNamedParameter("$brewery_id", brewery)
                .AddNamedParameter("$offset", index)
                .AddNamedParameter("$limit", limit));
        }
    }
}