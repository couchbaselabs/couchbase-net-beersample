using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Couchbase.BeerSample.Web.Controllers;
using Couchbase.BeerSample.Web.Models;
using NUnit.Framework;

namespace Couchbase.BeerSample.Web.Tests
{
    [TestFixture]
    public class BreweryControllerTests
    {
        [Test]
        public void Test_GetIndex()
        {
            using (var cluster = new CouchbaseCluster())
            {
                using (var bucket = cluster.OpenBucket("beer-sample"))
                {
                    var controller = new BreweryController(bucket);
                    var result = (ViewResult)controller.Index();
                    var breweries = result.Model as List<dynamic>;
                    Assert.AreEqual(10, breweries.Count);
                }
            }
        }
    }
}
