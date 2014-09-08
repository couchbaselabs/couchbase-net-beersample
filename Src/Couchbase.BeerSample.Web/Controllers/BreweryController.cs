using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Couchbase.Core;

namespace Couchbase.BeerSample.Web.Controllers
{
    public class BreweryController : Controller
    {
        private IBucket _bucket;

        public BreweryController() 
            : this(MvcApplication.Bucket)
        { 
        }

        public BreweryController(IBucket bucket)
        {
            _bucket = bucket;
        }
        //
        // GET: /Brewery/
        public ActionResult Index()
        {
            const string query = "SELECT META().id, b.name FROM beer-sample as b WHERE b.type='brewery' LIMIT 10";
            var result = _bucket.Query<dynamic>(query);
            return View(result.Rows);
        }

        //
        // GET: /Brewery/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Brewery/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Brewery/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Brewery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Brewery/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Brewery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Brewery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
