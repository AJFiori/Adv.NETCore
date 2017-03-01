using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class BridgeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Bridges";

            BridgeViewModel bridgeVM = new BridgeViewModel();
            using (var db = new BridgeDBContext())
            {
                bridgeVM.BridgeList = db.Bridges.ToList();
                bridgeVM.NewBridge = new Bridge();

                bridgeVM.MaterialDesigns = GetMaterialDesignsDDL();
                bridgeVM.ConstructionDesigns = GetConstructionDesignsDDL();
                bridgeVM.FunctionalClasses = GetFunctionalClassesDDL();
                bridgeVM.StatusCodes = GetStatusCodesDDL();                
                bridgeVM.Counties = GetCountiesDDL();
            }
            return View(bridgeVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(BridgeViewModel bridgeVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BridgeDBContext())
                {
                    db.Bridges.Add(bridgeVM.NewBridge);
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
            BridgeViewModel bridgeEditVM = new BridgeViewModel();
            using (BridgeDBContext db = new BridgeDBContext())
            {
                //assign view model object to value of current object id passed in route
                bridgeEditVM.NewBridge = db.Bridges.Where(
                    b => b.BridgeId == id).SingleOrDefault();
                //get DDL for select list
                bridgeEditVM.MaterialDesigns = GetMaterialDesignsDDL();
                bridgeEditVM.ConstructionDesigns = GetConstructionDesignsDDL();
                bridgeEditVM.FunctionalClasses = GetFunctionalClassesDDL();
                bridgeEditVM.StatusCodes = GetStatusCodesDDL();
                bridgeEditVM.Counties = GetCountiesDDL();
                //return the view model
                return View(bridgeEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(BridgeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (BridgeDBContext db = new BridgeDBContext())
                {
                    //instantiate object from view model
                    Bridge b = obj.NewBridge;
                    //retrieve primary key/id from route data
                    b.BridgeId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(b).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }

        //GetMaterialDesignsDDL
        private static List<SelectListItem> GetMaterialDesignsDDL()
        {
            List<SelectListItem> material = new List<SelectListItem>();
            MaterialDesignViewModel mdvm = new MaterialDesignViewModel();
            using (var db = new MaterialDesignDBContext())
            {
                mdvm.MaterialDesignList = db.MaterialDesigns.ToList();
            }
            foreach (MaterialDesign m in mdvm.MaterialDesignList)
            {
                material.Add(new SelectListItem
                {
                    Text = m.MaterialDesignType,
                    Value = m.MaterialDesignId.ToString()
                });
            }
            return material;
        }

        //GetConstructionDesignsDDL
        private static List<SelectListItem> GetConstructionDesignsDDL()
        {
            List<SelectListItem> construction = new List<SelectListItem>();
            ConstructionDesignViewModel cdvm = new ConstructionDesignViewModel();
            using (var db = new ConstructionDesignDBContext())
            {
                cdvm.ConstructionDesignList = db.ConstructionDesigns.ToList();
            }
            foreach (ConstructionDesign cd in cdvm.ConstructionDesignList)
            {
                construction.Add(new SelectListItem
                {
                    Text = cd.ConstructionDesignType,
                    Value = cd.ConstructionDesignId.ToString()
                });
            }
            return construction;
        }

        //GetFunctionalClassesDDL
        private static List<SelectListItem> GetFunctionalClassesDDL()
        {
            List<SelectListItem> functional = new List<SelectListItem>();
            FunctionalClassViewModel fcvm = new FunctionalClassViewModel();
            using (var db = new FunctionalClassDBContext())
            {
                fcvm.FunctionalClassList = db.FunctionalClasses.ToList();
            }
            foreach (FunctionalClass fc in fcvm.FunctionalClassList)
            {
                functional.Add(new SelectListItem
                {
                    Text = fc.FunctionalClassType,
                    Value = fc.FunctionalClassId.ToString()
                });
            }
            return functional;
        }

        //GetStatusCodesDDL
        private static List<SelectListItem> GetStatusCodesDDL()
        {
            List<SelectListItem> status = new List<SelectListItem>();
            StatusCodeViewModel scvm = new StatusCodeViewModel();
            using (var db = new StatusCodeDBContext())
            {
                scvm.StatusCodeList = db.StatusCodes.ToList();
            }
            foreach (StatusCode sc in scvm.StatusCodeList)
            {
                status.Add(new SelectListItem
                {
                    Text = sc.StatusName,
                    Value = sc.StatusCodeId.ToString()
                });
            }
            return status;
        }

        //GetCountiesDDL
        private static List<SelectListItem> GetCountiesDDL()
        {
            List<SelectListItem> county = new List<SelectListItem>();
            CountyViewModel cvm = new CountyViewModel();
            using (var db = new CountyDBContext())
            {
                cvm.CountyList = db.Counties.ToList();
            }
            foreach (County c in cvm.CountyList)
            {
                county.Add(new SelectListItem
                {
                    Text = c.CountyName,
                    Value = c.CountyId.ToString()
                });
            }
            return county;
        }
    }
}
