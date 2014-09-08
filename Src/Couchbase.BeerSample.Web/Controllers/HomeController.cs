using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Couchbase.Core;

namespace Couchbase.BeerSample.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /About/
        public ActionResult About()
        {
            return View();
        }
    }
}
