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
        private readonly IBucket _bucket;

        public BeerController() 
            : this(MvcApplication.Bucket)
        { 
        }

        public BeerController(IBucket bucket)
        {
            _bucket = bucket;
        }

        public ActionResult Index()
        {
            var query = _bucket.CreateQuery("beer", "all_beers").
                Limit(10);

            var result = _bucket.Query<dynamic>(query);
            return View(result.Rows);
        }

        [HttpPost]
        public ActionResult Index(int page)
        {
            var query = _bucket.CreateQuery("beer", "all_beers").
                Skip(page).
                Limit(10);

            var result = _bucket.Query<dynamic>(query);
            return View(result.Rows);
        }

        public ActionResult Details(string id)
        {
            var result = _bucket.GetDocument<Beer>(id);
            ViewBag.Success = result.Success;
            ViewBag.Message = result.Message;
            ViewBag.Status = result.Status;

            return View(result.Value);
        }

        public ActionResult Create()
        {
            ViewBag.Success = true;
            ViewBag.Message = "";
            return View(new Beer());
        }

        [HttpPost]
        public ActionResult Create(Beer beer)
        {
            try
            {
                beer.Type = "beer";
                beer.Updated = DateTime.Now;
                var result = _bucket.Insert(new Document<Beer>
                {
                    Id = beer.Name.Replace(' ', '_').ToLower(),
                    Value = beer
                });

                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Success = result.Success;
                ViewBag.Message = result.Message;
                ViewBag.Status = result.Status;
                return View(result.Value);
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            var result = _bucket.GetDocument<Beer>(id);
            ViewBag.Success = result.Success;
            ViewBag.Message = result.Message;
            ViewBag.Status = result.Status;

            return View(result.Value);
        }

        [HttpPost]
        public ActionResult Edit(string id, Beer modified)
        {
            try
            {
                modified.Updated = DateTime.Now;
                var result = _bucket.Upsert(new Document<Beer>
                {
                    Id = id,
                    Value = modified
                });

                if (result.Success)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Success = result.Success;
                ViewBag.Message = result.Message;
                ViewBag.Status = result.Status;
                return View(modified);
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            var result = _bucket.GetDocument<Beer>(id);
            ViewBag.Success = result.Success;
            ViewBag.Message = result.Message;
            ViewBag.Status = result.Status;

            return View(result.Value);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var result = _bucket.GetDocument<Beer>(id);
                if (result.Success)
                {
                    var document = result.Document;
                    var deleted = _bucket.Remove(document);
                    if (deleted.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ViewBag.Success = deleted.Success;
                    ViewBag.Message = deleted.Message;
                    ViewBag.Status = deleted.Status;
                    return View();
                }
                ViewBag.Success = result.Success;
                ViewBag.Message = result.Message;
                ViewBag.Status = result.Status;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }
    }
}
