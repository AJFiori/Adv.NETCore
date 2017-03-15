using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class MaintenanceRecordController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "MaintenanceRecords";

            MaintenanceRecordViewModel maintenanceRecordVM = new MaintenanceRecordViewModel();
            using (var db = new MaintenanceRecordDBContext())
            {
                maintenanceRecordVM.MaintenanceRecordList = db.MaintenanceRecords.ToList();
                maintenanceRecordVM.NewMaintenanceRecord = new MaintenanceRecord();
                maintenanceRecordVM.Inspectors = GetInspectorsDLL();
                maintenanceRecordVM.MaintenanceActions = GetMaintenanceActionsDLL();

            }
            return View(maintenanceRecordVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(MaintenanceRecordViewModel maintenanceRecordVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaintenanceRecordDBContext())
                {
                    db.MaintenanceRecords.Add(maintenanceRecordVM.NewMaintenanceRecord);
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
            MaintenanceRecordViewModel mrVM = new MaintenanceRecordViewModel();
            using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
            {
                //assign view model object to value of current object id passed in route
                mrVM.NewMaintenanceRecord = db.MaintenanceRecords.Where(
                    mr => mr.MaintenanceRecordId == id).SingleOrDefault();
                //get DDL for select list
                mrVM.Inspectors = GetInspectorsDLL();
                mrVM.MaintenanceActions = GetMaintenanceActionsDLL();
                //return the view model
                return View(mrVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaintenanceRecordViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
                {
                    //instantiate object from view model
                    MaintenanceRecord mr = obj.NewMaintenanceRecord;
                    //retrieve primary key/id from route data
                    mr.MaintenanceRecordId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(mr).State = EntityState.Modified;
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
            MaintenanceRecordViewModel mrVm = new MaintenanceRecordViewModel();
            using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
            {
                //instantiate object from view model
                mrVm.NewMaintenanceRecord = new MaintenanceRecord();
                //retrieve primary key/id from route data
                mrVm.NewMaintenanceRecord.MaintenanceRecordId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(mrVm.NewMaintenanceRecord).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
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
                    Text = ins.InspectorFirst + " " + ins.InspectorLast + " :"+ ins.InspectorOrg,
                    Value = ins.InspectorId.ToString()
                });
            }
            return inspector;
        }

        //GetMaintenanceActionsDLL
        private static List<SelectListItem> GetMaintenanceActionsDLL()
        {
            List<SelectListItem> mAction = new List<SelectListItem>();
            MaintenanceActionViewModel mavm = new MaintenanceActionViewModel();
            using (var db = new MaintenanceActionDBContext())
            {
                mavm.MaintenanceActionList = db.MaintenanceActions.ToList();
            }
            foreach (MaintenanceAction ma in mavm.MaintenanceActionList)
            {
                mAction.Add(new SelectListItem
                {
                    Text = ma.MaintenanceActionName,
                    Value = ma.MaintenanceActionId.ToString()
                });
            }
            return mAction;
        }

    }
}
