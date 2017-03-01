using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class InspectorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Inspectors";

            InspectorViewModel inspectorVM = new InspectorViewModel();
            using (var db = new InspectorDBContext())
            {
                inspectorVM.InspectorList = db.Inspectors.ToList();
                inspectorVM.NewInspector = new Inspector();
            }
            return View(inspectorVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(InspectorViewModel inspectorVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectorDBContext())
                {
                    db.Inspectors.Add(inspectorVM.NewInspector);
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
            InspectorViewModel insEditVM = new InspectorViewModel();
            using (InspectorDBContext db = new InspectorDBContext())
            {
                //assign view model object to value of current object id passed in route
                insEditVM.NewInspector = db.Inspectors.Where(
                    ins => ins.InspectorId == id).SingleOrDefault();
                //return the view model
                return View(insEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(InspectorViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (InspectorDBContext db = new InspectorDBContext())
                {
                    //instantiate object from view model
                    Inspector ins = obj.NewInspector;
                    //retrieve primary key/id from route data
                    ins.InspectorId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(ins).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
    }
}
