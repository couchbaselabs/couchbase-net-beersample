using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Core;

namespace Couchbase.BeerSample.Domain.Persistence
{
    public sealed class BeerDao : BaseDao<Beer>
    {
        public BeerDao(IBucket bucket) 
            : base(bucket)
        {
        }
    }
}
