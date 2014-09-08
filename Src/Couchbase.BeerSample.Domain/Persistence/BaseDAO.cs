using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Core;
using Couchbase.Views;

namespace Couchbase.BeerSample.Domain.Persistence
{
    public abstract class BaseDao<T> where T : IDocument<T>
    {
        private readonly IBucket _bucket;

        protected BaseDao(IBucket bucket)
        {
            _bucket = bucket;
        }

        public virtual T Insert(T entity)
        {
           /* var document = new Document<Beer>
            {
                Id = beer.Name,
                Value = beer
            };

            var result = _bucket.Insert(document);
            if (result.Success)
            {
                
            }*/
            throw new NotImplementedException();
        }

        public virtual T Upsert(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T Replace(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> QueryView(IViewQuery query)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> QueryN1Ql(string query)
        {
            throw new NotImplementedException();
        }
    }
}
