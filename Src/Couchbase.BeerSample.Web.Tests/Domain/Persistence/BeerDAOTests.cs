using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.BeerSample.Domain;
using NUnit.Framework;
using Beer = Couchbase.BeerSample.Web.Models.Beer;

namespace Couchbase.BeerSample.Web.Tests.Domain.Persistence
{
    [TestFixture]
    public class BeerDaoTests
    {
        [Test]
        public void TestInsert()
        {
            var beer = new Beer();
        }
    }
}
