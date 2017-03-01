using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class StatusCodeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Status Code";

            StatusCodeViewModel statusCodeVM = new StatusCodeViewModel();
            using (var db = new StatusCodeDBContext())
            {
                statusCodeVM.StatusCodeList = db.StatusCodes.ToList();
                statusCodeVM.NewStatusCode = new StatusCode();
            }
            return View(statusCodeVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(StatusCodeViewModel statusCodeVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new StatusCodeDBContext())
                {
                    db.StatusCodes.Add(statusCodeVM.NewStatusCode);
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
            StatusCodeViewModel scEditVM = new StatusCodeViewModel();
            using (StatusCodeDBContext db = new StatusCodeDBContext())
            {
                //assign view model object to value of current object id passed in route
                scEditVM.NewStatusCode = db.StatusCodes.Where(
                    sc => sc.StatusCodeId == id).SingleOrDefault();
                //return the view model
                return View(scEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(StatusCodeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (StatusCodeDBContext db = new StatusCodeDBContext())
                {
                    //instantiate object from view model
                    StatusCode sc = obj.NewStatusCode;
                    //retrieve primary key/id from route data
                    sc.StatusCodeId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(sc).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
    }
}

