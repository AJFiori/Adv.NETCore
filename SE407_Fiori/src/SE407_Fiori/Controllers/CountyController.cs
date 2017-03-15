using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class CountyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "County";

            CountyViewModel CountyVM = new CountyViewModel();
            using (var db = new CountyDBContext())
            {
                CountyVM.CountyList = db.Counties.ToList();
                CountyVM.NewCounty = new County();
            }
            return View(CountyVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(CountyViewModel CountyVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CountyDBContext())
                {
                    db.Counties.Add(CountyVM.NewCounty);
                    db.SaveChanges();
                }
            }
            // Redirect to get Index GET method
            return RedirectToAction("Index");
        }

        //GET  This is for edit action to populate form with current values
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            CountyViewModel countyEditVM = new CountyViewModel();
            using (CountyDBContext db = new CountyDBContext())
            {
                //assign view model object to value of current object id passed in route
                countyEditVM.NewCounty = db.Counties.Where(
                    c => c.CountyId == id).SingleOrDefault();
                //return the view model
                return View(countyEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(CountyViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (CountyDBContext db = new CountyDBContext())
                {
                    //instantiate object from view model
                    County c = obj.NewCounty;
                    //retrieve primary key/id from route data
                    c.CountyId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(c).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }

        //GET - DELETE OPERATION
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            CountyViewModel countyDeleteVM = new CountyViewModel();
            using (CountyDBContext db = new CountyDBContext())
            {
                using (var dbb = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbb.Bridges.ToList();
                    bridgeVm.NewBridge = dbb.Bridges.Where(
                    b => b.CountyId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        countyDeleteVM.NewCounty = new County();
                        //retrieve primary key/id from route data
                        countyDeleteVM.NewCounty.CountyId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(countyDeleteVM.NewCounty).State =
                            EntityState.Deleted;
                        //persist changes
                        db.SaveChanges();
                        TempData["ResultMessage"] = "Object deleted";
                    }
                    else
                    {
                        TempData["ResultMessage"] =
                            "Object has dependencies, cannot delete!!!!";
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}

