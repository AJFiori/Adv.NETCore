using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class FunctionalClassController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Functional Class";

            FunctionalClassViewModel functionalClassVM = new FunctionalClassViewModel();
            using (var db = new FunctionalClassDBContext())
            {
                functionalClassVM.FunctionalClassList = db.FunctionalClasses.ToList();
                functionalClassVM.NewFunctionalClass = new FunctionalClass();
            }
            return View(functionalClassVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(FunctionalClassViewModel functionalClassVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FunctionalClassDBContext())
                {
                    db.FunctionalClasses.Add(functionalClassVM.NewFunctionalClass);
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
            FunctionalClassViewModel functionalEditVM = new FunctionalClassViewModel();
            using (FunctionalClassDBContext db = new FunctionalClassDBContext())
            {
                //assign view model object to value of current object id passed in route
                functionalEditVM.NewFunctionalClass = db.FunctionalClasses.Where(
                    f => f.FunctionalClassId == id).SingleOrDefault();
                //return the view model
                return View(functionalEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(FunctionalClassViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (FunctionalClassDBContext db = new FunctionalClassDBContext())
                {
                    //instantiate object from view model
                    FunctionalClass f = obj.NewFunctionalClass;
                    //retrieve primary key/id from route data
                    f.FunctionalClassId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(f).State = EntityState.Modified;
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
            FunctionalClassViewModel functionalDeleteVM = new FunctionalClassViewModel();
            using (FunctionalClassDBContext db = new FunctionalClassDBContext())
            {
                using (var dbb = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbb.Bridges.ToList();
                    bridgeVm.NewBridge = dbb.Bridges.Where(
                    b => b.FunctionalClassId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        functionalDeleteVM.NewFunctionalClass = new FunctionalClass();
                        //retrieve primary key/id from route data
                        functionalDeleteVM.NewFunctionalClass.FunctionalClassId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(functionalDeleteVM.NewFunctionalClass).State =
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

