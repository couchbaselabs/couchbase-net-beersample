using System;
using System.Web.Mvc;
using Couchbase.BeerSample.Domain;
using Couchbase.BeerSample.Domain.Exceptions;
using Couchbase.BeerSample.Domain.Persistence;
using Couchbase.BeerSample.Web.Models;

namespace Couchbase.BeerSample.Web.Controllers
{
    public class BreweryController : Controller
    {
        protected readonly BreweryRepository Repository;

        public BreweryController() :
            this(new BreweryRepository(ClusterHelper.GetBucket("beer-sample")))
        {
        }

        public BreweryController(BreweryRepository repository)
        {
            Repository = repository;
        }

        public ActionResult Index()
        {
            try
            {
                return View(Repository.SelectAllBreweries(0, 10));
            }
            catch (QueryRequestException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.QueryStatus;
                return View();
            }
        }

        public ActionResult Details(string id)
        {
            Brewery brewery = null;
            try
            {
                brewery = Repository.Find(id);
                ViewBag.Success = true;
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
            }
            return View(new BreweryViewModel(brewery));
        }

        public ActionResult Create()
        {
            ViewBag.Success = true;
            ViewBag.Message = "";
            return View(new Beer());
        }

        [HttpPost]
        public ActionResult Create(Brewery brewery)
        {
            try
            {
                brewery.Type = "beer";
                brewery.Updated = DateTime.Now;
                brewery.Id = brewery.Name.Replace(' ', '_').ToLower();
                Repository.Save(brewery);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View(brewery);
            }
        }

        public ActionResult Edit(string id)
        {
            Brewery brewery = null;
            try
            {
                brewery = Repository.Find(id);
                ViewBag.Success = true;
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
            }
            return View(new BreweryViewModel(brewery));
        }

        [HttpPost]
        public ActionResult Edit(string id, BreweryViewModel viewModel)
        {
            try
            {
                var brewery = Repository.Find(id);
                viewModel.Update(brewery);
                Repository.Save(brewery);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View(viewModel);
            }
        }

        public ActionResult Delete(string id)
        {
            Brewery brewery = null;
            try
            {
                brewery = Repository.Find(id);
                Repository.Remove(brewery);
                return RedirectToAction("Index");
            }
            catch (CouchbaseDataException e)
            {
                ViewBag.Success = false;
                ViewBag.Message = e.Message;
                ViewBag.Status = e.Status;
                return View(brewery);
            }
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var brewery = Repository.Find(id);
                Repository.Remove(brewery);
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
