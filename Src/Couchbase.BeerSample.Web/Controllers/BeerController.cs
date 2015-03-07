using System;
using System.Web.Mvc;
using Couchbase.BeerSample.Domain;
using Couchbase.BeerSample.Domain.Exceptions;
using Couchbase.BeerSample.Domain.Persistence;

namespace Couchbase.BeerSample.Web.Controllers
{
    public class BeerController : Controller
    {
        protected readonly BeerRepository Repository;

        public BeerController()
            : this(new BeerRepository(ClusterHelper.GetBucket("beer-sample")))
        {
        }

        public BeerController(BeerRepository repository)
        {
            Repository = repository;
        }

        public ActionResult Index()
        {

            return View(Repository.SelectBeers(0, 10));
        }

        [HttpPost]
        public ActionResult Index(int page)
        {
            return View(Repository.SelectBeers(page, 10));
        }

        public ActionResult Details(string id)
        {
            Beer beer = null;
            try
            {
                beer = Repository.Find(id);
                ViewBag.Success = true;
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
            }
            return View(beer);
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
                beer.Id = beer.Name.Replace(' ', '_').ToLower();
                Repository.Save(beer);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View(beer);
            }
        }

        public ActionResult Edit(string id)
        {
            Beer beer = null;
            try
            {
                beer = Repository.Find(id);
                ViewBag.Success = true;
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
            }
            return View(beer);
        }

        [HttpPost]
        public ActionResult Edit(string id, Beer modified)
        {
            try
            {
                modified.Updated = DateTime.Now;
                Repository.Save(modified);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View(modified);
            }
        }

        public ActionResult Delete(string id)
        {
            Beer beer = null;
            try
            {
                beer = Repository.Find(id);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View(beer);
            }
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var beer = Repository.Find(id);
                Repository.Remove(beer);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View();
            }
        }
    }
}
