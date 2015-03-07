using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Newtonsoft.Json;

namespace Couchbase.BeerSample.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = new ClientConfiguration
            {
                SerializationSettings = new JsonSerializerSettings
                {
                    ContractResolver = 
                }
            };
            ClusterHelper.Initialize(config);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            ClusterHelper.Close();
        }
    }
}
