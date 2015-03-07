
using System.Collections.Generic;
using System.Linq;
using Couchbase.BeerSample.Domain.Exceptions;
using Couchbase.BeerSample.Domain.Extensions;
using Couchbase.Core;
using Couchbase.N1QL;
using Couchbase.Views;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Domain.Persistence.Core
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        public Repository(IBucket bucket)
        {
            Bucket = bucket;
        }

        protected  IBucket Bucket { get; set; }

        public void Save(T entity)
        {
            var result = Bucket.Upsert(entity.Wrap());
            result.ThrowIfNotSuccess();
        }

        public void Remove(T entity)
        {
            var result = Bucket.Remove(entity.Wrap());
            result.ThrowIfNotSuccess(entity.Id);
        }

        public IEnumerable<T> Select(IQueryRequest queryRequest)
        {
            var results = Bucket.Query<T>(queryRequest);
            if (!results.Success)
            {
                var message = JsonConvert.SerializeObject(results.Errors);
                throw new QueryRequestException(message, results.Status);
            }
            return results.Rows;
        }

        public IEnumerable<T> Select(IViewQuery viewQuery)
        {
            var results = Bucket.Query<T>(viewQuery);
            if (!results.Success)
            {
                var message = results.Error;
                throw new ViewRequestException(message, results.StatusCode);
            }
            return results.Values;
        }

        public T Find(string key)
        {
            var result = Bucket.GetDocument<T>(key);
            result.ThrowIfNotSuccess();
            return result.Document.UnWrap();
        }

        public IEnumerable<T> Select(IList<string> keys)
        {
            throw new System.NotImplementedException();
        }
    }
}