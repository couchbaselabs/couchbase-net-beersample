using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Couchbase.BeerSample.Web.Models;
using Couchbase.Core;

namespace Couchbase.BeerSample.Web.Controllers
{
    public class BeerController : Controller
    {
        private IBucket _bucket;

        public BeerController() 
            : this(MvcApplication.Bucket)
        { 
        }

        public BeerController(IBucket bucket)
        {
            _bucket = bucket;
        }

        //
        // GET: /Beer/
        public ActionResult Index()
        {
            var query = _bucket.CreateQuery(true).
                DesignDoc("beer").
                View("all_beers").
                Limit(10);

            var result = _bucket.Query<dynamic>(query);
            return View(result.Rows);
        }

        //
        // POST: /Beer/
        [HttpPost]
        public ActionResult Index(int page)
        {
            var query = _bucket.CreateQuery(false).
                DesignDoc("beer").
                View("all_beers").
                Skip(page).
                Limit(10);

            var result = _bucket.Query<Beer>(query);
            return View(result.Rows);
        }

        //
        // GET: /Beer/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        //
        // GET: /Beer/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Beer/Create
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
        // GET: /Beer/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        //
        // POST: /Beer/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
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
        // GET: /Beer/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        //
        // POST: /Beer/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
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
