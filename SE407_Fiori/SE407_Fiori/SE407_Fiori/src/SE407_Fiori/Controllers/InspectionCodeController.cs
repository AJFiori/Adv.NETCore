﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_Fiori.Controllers
{
    public class InspectionCodeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "InspectionCodes";

            InspectionCodeViewModel inspectionCodeVM = new InspectionCodeViewModel();
            using (var db = new InspectionCodeDBContext())
            {
                inspectionCodeVM.InspectionCodeList = db.InspectionCodes.ToList();
                inspectionCodeVM.NewInspectionCode = new InspectionCode();
            }
            return View(inspectionCodeVM);
        }

        //POST:
        [HttpPost]
        public IActionResult Index(InspectionCodeViewModel inspectionCodeVM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectionCodeDBContext())
                {
                    db.InspectionCodes.Add(inspectionCodeVM.NewInspectionCode);
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
            InspectionCodeViewModel inCodeEditVM = new InspectionCodeViewModel();
            using (InspectionCodeDBContext db = new InspectionCodeDBContext())
            {
                //assign view model object to value of current object id passed in route
                inCodeEditVM.NewInspectionCode = db.InspectionCodes.Where(
                    inc => inc.InspectionCodeId == id).SingleOrDefault();
                //return the view model
                return View(inCodeEditVM);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(InspectionCodeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (InspectionCodeDBContext db = new InspectionCodeDBContext())
                {
                    //instantiate object from view model
                    InspectionCode inc = obj.NewInspectionCode;
                    //retrieve primary key/id from route data
                    inc.InspectionCodeId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(inc).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
    }
}

