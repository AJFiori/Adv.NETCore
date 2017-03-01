using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SE407_Fiori.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Employees";

            EmployeeViewModel employeeVM = new EmployeeViewModel();
            using (var db = new EmployeeDBContext())
            {
                employeeVM.EmployeeList = db.Employees.ToList();
                employeeVM.NewEmployee = new Employee();
            }
            return View(employeeVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new EmployeeDBContext())
                {
                    db.Employees.Add(employeeVM.NewEmployee);
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
            EmployeeViewModel employeeEditVM = new EmployeeViewModel();
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                //assign view model object to value of current object id passed in route
                employeeEditVM.NewEmployee = db.Employees.Where(
                    e => e.EmployeeId == id).SingleOrDefault();
                //return the view model
                return View(employeeEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (EmployeeDBContext db = new EmployeeDBContext())
                {
                    //instantiate object from view model
                    Employee e = obj.NewEmployee;
                    //retrieve primary key/id from route data
                    e.EmployeeId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(e).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
    }
}
