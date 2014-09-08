using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Couchbase.BeerSample.Web.Models;
using Couchbase.BeerSample.Web.Controllers;
using Couchbase.Views;
using NUnit.Framework;

namespace Couchbase.BeerSample.Web.Tests
{
    [TestFixture]
    public class BeerControllerTests
    {
        [Test]
        public void Test_GetIndex()
        {
            using (var cluster = new CouchbaseCluster())
            {
                using (var bucket = cluster.OpenBucket("beer-sample"))
                {
                    var controller = new BeerController(bucket);
                    var result = (ViewResult)controller.Index();
                    var beers = result.Model as List<dynamic>;
                    Assert.AreEqual(10, beers.Count);
                }
            }
        }
    }
}
