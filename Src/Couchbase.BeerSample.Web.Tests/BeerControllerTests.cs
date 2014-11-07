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
using NUnit.Framework.Constraints;

namespace Couchbase.BeerSample.Web.Tests
{
    [TestFixture]
    public class BeerControllerTests
    {
        [Test]
        public void Test_Get_Index()
        {
            using (var cluster = new Cluster())
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

        [Test]
        public void Test_Get_Details()
        {
            const string id = "21st_amendment_brewery_cafe-bitter_american";
            using (var cluster = new Cluster())
            {
                using (var bucket = cluster.OpenBucket("beer-sample"))
                {
                    var controller = new BeerController(bucket);
                    var result = (ViewResult) controller.Details(id);
                    var beer = result.Model as Beer;
                    Assert.IsNotNull(beer);
                }
            }
        }

        [Test]
        public void Test_Create()
        {
            using (var cluster = new Cluster())
            {
                using (var bucket = cluster.OpenBucket("beer-sample"))
                {
                    bucket.Remove("skunky_beer");
                    var controller = new BeerController(bucket);
                    var result = (RedirectToRouteResult)controller.Create(new Beer { Name = "skunky beer" });
                    Assert.IsNotNull(result);
                }
            }
        }

        [Test]
        public void Test_Edit()
        {
            const string id = "21st_amendment_brewery_cafe-amendment_pale_ale";
            using (var cluster = new Cluster())
            {
                using (var bucket = cluster.OpenBucket("beer-sample"))
                {
                    var controller = new BeerController(bucket);
                    var get = bucket.GetDocument<Beer>(id);
                    var beer = get.Value;
                    beer.Ibu = 3.8m;
                    var result = (RedirectToRouteResult) controller.Edit(id, get.Value);
                    Assert.IsNotNull(result);
                }
            }
        }
    }
}
