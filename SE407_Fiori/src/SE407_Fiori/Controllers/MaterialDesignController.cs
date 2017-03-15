using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class MaterialDesignController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Material Design";

            MaterialDesignViewModel materialDesignVM = new MaterialDesignViewModel();
            using (var db = new MaterialDesignDBContext())
            {
                materialDesignVM.MaterialDesignList = db.MaterialDesigns.ToList();
                materialDesignVM.NewMaterialDesign = new MaterialDesign();
            }
            return View(materialDesignVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(MaterialDesignViewModel materialDesignVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaterialDesignDBContext())
                {
                    db.MaterialDesigns.Add(materialDesignVM.NewMaterialDesign);
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
            MaterialDesignViewModel mdEditVM = new MaterialDesignViewModel();
            using (MaterialDesignDBContext db = new MaterialDesignDBContext())
            {
                //assign view model object to value of current object id passed in route
                mdEditVM.NewMaterialDesign = db.MaterialDesigns.Where(
                    md => md.MaterialDesignId == id).SingleOrDefault();
                //return the view model
                return View(mdEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaterialDesignViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (MaterialDesignDBContext db = new MaterialDesignDBContext())
                {
                    //instantiate object from view model
                    MaterialDesign md = obj.NewMaterialDesign;
                    //retrieve primary key/id from route data
                    md.MaterialDesignId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(md).State = EntityState.Modified;
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
            MaterialDesignViewModel materialDeleteVM = new MaterialDesignViewModel();
            using (MaterialDesignDBContext db = new MaterialDesignDBContext())
            {
                using (var dbb = new BridgeDBContext())
                {
                    BridgeViewModel bridgeVm = new BridgeViewModel();
                    bridgeVm.BridgeList = dbb.Bridges.ToList();
                    bridgeVm.NewBridge = dbb.Bridges.Where(
                    b => b.MaterialDesignId == id).FirstOrDefault();
                    //instantiate object from view model
                    if (bridgeVm.NewBridge == null)
                    {
                        materialDeleteVM.NewMaterialDesign = new MaterialDesign();
                        //retrieve primary key/id from route data
                        materialDeleteVM.NewMaterialDesign.MaterialDesignId =
                            Guid.Parse(RouteData.Values["id"].ToString());
                        //mark the record as modified
                        db.Entry(materialDeleteVM.NewMaterialDesign).State =
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
