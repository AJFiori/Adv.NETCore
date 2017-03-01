
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class MaintenanceActionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "MaintenanceAction";

            MaintenanceActionViewModel maintenanceActionVM = new MaintenanceActionViewModel();
            using (var db = new MaintenanceActionDBContext())
            {
                maintenanceActionVM.MaintenanceActionList = db.MaintenanceActions.ToList();
                maintenanceActionVM.NewMaintenanceAction = new MaintenanceAction();
            }
            return View(maintenanceActionVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(MaintenanceActionViewModel maintenanceActionVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaintenanceActionDBContext())
                {
                    db.MaintenanceActions.Add(maintenanceActionVM.NewMaintenanceAction);
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
            MaintenanceActionViewModel maEditVM = new MaintenanceActionViewModel();
            using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
            {
                //assign view model object to value of current object id passed in route
                maEditVM.NewMaintenanceAction = db.MaintenanceActions.Where(
                    ma => ma.MaintenanceActionId == id).SingleOrDefault();
                //return the view model
                return View(maEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaintenanceActionViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
                {
                    //instantiate object from view model
                    MaintenanceAction ma = obj.NewMaintenanceAction;
                    //retrieve primary key/id from route data
                    ma.MaintenanceActionId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(ma).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
    }
}

