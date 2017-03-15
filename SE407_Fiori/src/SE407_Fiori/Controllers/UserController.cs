using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "Users";

            UserViewModel userVM = new UserViewModel();
            using (var db = new UserDBContext())
            {
                userVM.UserList = db.Users.ToList();
                userVM.NewUser = new User();
            }
            return View(userVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new UserDBContext())
                {
                    db.Users.Add(userVM.NewUser);
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
            UserViewModel userEditVM = new UserViewModel();
            using (UserDBContext db = new UserDBContext())
            {
                //assign view model object to value of current object id passed in route
                userEditVM.NewUser = db.Users.Where(
                    u => u.UserID == id).SingleOrDefault();
                //return the view model
                return View(userEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(UserViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (UserDBContext db = new UserDBContext())
                {
                    //instantiate object from view model
                    User u = obj.NewUser;
                    //retrieve primary key/id from route data
                    u.UserID = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(u).State = EntityState.Modified;
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
            UserViewModel userVm = new UserViewModel();
            using (UserDBContext db = new UserDBContext())
            {
                //instantiate object from view model
                userVm.NewUser = new User();
                //retrieve primary key/id from route data
                userVm.NewUser.UserID =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(userVm.NewUser).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
