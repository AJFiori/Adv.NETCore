using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class ConstructionDesignController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Construction Design";

            ConstructionDesignViewModel ConstructionDesignVM = new ConstructionDesignViewModel();
            using (var db = new ConstructionDesignDBContext())
            {
                ConstructionDesignVM.ConstructionDesignList = db.ConstructionDesigns.ToList();
                ConstructionDesignVM.NewConstructionDesign = new ConstructionDesign();
            }
            return View(ConstructionDesignVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(ConstructionDesignViewModel ConstructionDesignVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ConstructionDesignDBContext())
                {
                    db.ConstructionDesigns.Add(ConstructionDesignVM.NewConstructionDesign);
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
            ConstructionDesignViewModel constructionEditVM = new ConstructionDesignViewModel();
            using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
            {
                //assign view model object to value of current object id passed in route
                constructionEditVM.NewConstructionDesign = db.ConstructionDesigns.Where(
                    c => c.ConstructionDesignId == id).SingleOrDefault();
                //return the view model
                return View(constructionEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(ConstructionDesignViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
                {
                    //instantiate object from view model
                    ConstructionDesign c = obj.NewConstructionDesign;
                    //retrieve primary key/id from route data
                    c.ConstructionDesignId = Guid.Parse(RouteData.Values["id"].ToString());
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
            ConstructionDesignViewModel constructionDeleteVM = new ConstructionDesignViewModel();
            using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
            {
                using (var dbb = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbb.Bridges.ToList();
                    bridgeVm.NewBridge = dbb.Bridges.Where(
                    b => b.ConstructionDesignId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        constructionDeleteVM.NewConstructionDesign = new ConstructionDesign();
                        //retrieve primary key/id from route data
                        constructionDeleteVM.NewConstructionDesign.ConstructionDesignId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(constructionDeleteVM.NewConstructionDesign).State =
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
