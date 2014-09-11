using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Couchbase.BeerSample.Web.Models;
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

        public ActionResult Index()
        {
            const string query = "SELECT META().id, b.name FROM beer-sample as b WHERE b.type='brewery' LIMIT 10";
            var result = _bucket.Query<dynamic>(query);
            ViewBag.Success = result.Success;
            ViewBag.Message = result.Message;
            return View(result.Rows);
        }

        public ActionResult Details(string id)
        {
            var result = _bucket.GetDocument<Brewery>(id);
            ViewBag.Success = result.Success;
            ViewBag.Message = result.Message;
            ViewBag.Status = result.Status;

            return View(result.Value);
        }

        public ActionResult Create()
        {
            ViewBag.Success = true;
            ViewBag.Message = "";
            return View(new Brewery());
        }

        [HttpPost]
        public ActionResult Create(Brewery brewery)
        {
            try
            {
                var result = _bucket.Insert(new Document<Brewery>
                {
                    Id = brewery.Name.Replace(' ', '_').ToLower(),
                    Value = brewery
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
            var result = _bucket.GetDocument<Brewery>(id);
            ViewBag.Success = result.Success;
            ViewBag.Message = result.Message;
            ViewBag.Status = result.Status;

            return View(result.Value);
        }

        [HttpPost]
        public ActionResult Edit(string id, Brewery modified)
        {
            try
            {
                var result = _bucket.Upsert(new Document<Brewery>
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
            var result = _bucket.GetDocument<Brewery>(id);
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
            catch(Exception e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }
    }
}
