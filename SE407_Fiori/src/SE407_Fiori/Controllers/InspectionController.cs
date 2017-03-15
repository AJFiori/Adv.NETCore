using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class InspectionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Inspection";

            InspectionViewModel inspectionVM = new InspectionViewModel();
            using (var db = new InspectionDBContext())
            {
                inspectionVM.InspectionList = db.Inspections.ToList();
                inspectionVM.NewInspection = new Inspection();

                inspectionVM.Bridges = GetBridgesDDL();
                inspectionVM.Inspectors = GetInspectorsDLL();
                inspectionVM.InspectionCodes = GetInspectionCodesDDL();
            }
            return View(inspectionVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(InspectionViewModel inspectionVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectionDBContext())
                {
                    db.Inspections.Add(inspectionVM.NewInspection);
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
            InspectionViewModel inspectionVM = new InspectionViewModel();
            using (InspectionDBContext db = new InspectionDBContext())
            {
                //assign view model object to value of current object id passed in route
                inspectionVM.NewInspection = db.Inspections.Where(
                    ins => ins.InspectionId == id).SingleOrDefault();
                //get DDL for select list
                inspectionVM.Bridges = GetBridgesDDL();
                inspectionVM.Inspectors = GetInspectorsDLL();
                inspectionVM.InspectionCodes = GetInspectionCodesDDL();
                //return the view model
                return View(inspectionVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(InspectionViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (InspectionDBContext db = new InspectionDBContext())
                {
                    //instantiate object from view model
                    Inspection ins = obj.NewInspection;
                    //retrieve primary key/id from route data
                    ins.InspectionId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(ins).State = EntityState.Modified;
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
            InspectionViewModel inspectionVm = new InspectionViewModel();
            using (InspectionDBContext db = new InspectionDBContext())
            {
                //instantiate object from view model
                inspectionVm.NewInspection = new Inspection();
                //retrieve primary key/id from route data
                inspectionVm.NewInspection.InspectionId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(inspectionVm.NewInspection).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }

        //GetBridgesDDL
        private static List<SelectListItem> GetBridgesDDL()
        {
            List<SelectListItem> bridge = new List<SelectListItem>();
            BridgeViewModel bvm = new BridgeViewModel();
            using (var db = new BridgeDBContext())
            {
                bvm.BridgeList = db.Bridges.ToList();
            }
            foreach (Bridge b in bvm.BridgeList)
            {
                bridge.Add(new SelectListItem
                {
                    Text = b.CarriedFeature + " - " + b.IntersectingFeature,
                    Value = b.BridgeId.ToString()
                });
            }
            return bridge;
        }

        //GetInspectorsDLL
        private static List<SelectListItem> GetInspectorsDLL()
        {
            List<SelectListItem> inspector = new List<SelectListItem>();
            InspectorViewModel invm = new InspectorViewModel();
            using (var db = new InspectorDBContext())
            {
                invm.InspectorList = db.Inspectors.ToList();
            }
            foreach (Inspector ins in invm.InspectorList)
            {
                inspector.Add(new SelectListItem
                {
                    Text = ins.InspectorFirst + " " + ins.InspectorLast + " :" + ins.InspectorOrg,
                    Value = ins.InspectorId.ToString()
                });
            }
            return inspector;
        }


        //GetInspectionCodesDDL
        private static List<SelectListItem> GetInspectionCodesDDL()
        {
            List<SelectListItem> inspectionCodes = new List<SelectListItem>();
            InspectionCodeViewModel icvm = new InspectionCodeViewModel();
            using (var db = new InspectionCodeDBContext())
            {
                icvm.InspectionCodeList = db.InspectionCodes.ToList();
                
            }
            foreach (InspectionCode ic in icvm.InspectionCodeList)
            {
                inspectionCodes.Add(new SelectListItem
                {
                    Text = ic.InspectionCodeName,
                    Value = ic.InspectionCodeId.ToString()
                });
            }
            return inspectionCodes;
        }
        
    }
}
