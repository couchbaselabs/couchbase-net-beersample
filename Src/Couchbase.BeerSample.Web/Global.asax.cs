using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Couchbase.Core;

namespace Couchbase.BeerSample.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static CouchbaseCluster Cluster { get; set; }
        public static IBucket Bucket;
        protected void Application_Start()
        {
            Cluster = new CouchbaseCluster();
            Bucket = Cluster.OpenBucket("beer-sample");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
