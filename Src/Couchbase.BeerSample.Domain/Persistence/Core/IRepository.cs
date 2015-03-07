using System.Collections.Generic;
using Couchbase.N1QL;
using Couchbase.Views;

namespace Couchbase.BeerSample.Domain.Persistence.Core
{
    public interface IRepository<T> where T : IEntity
    {
        void Save(T entity);
        void Remove(T entity);
        IEnumerable<T> Select(IList<string> keys);
        IEnumerable<T> Select(IQueryRequest queryRequest);
        IEnumerable<T> Select(IViewQuery viewQuery);
        T Find(string key);
    }
}
