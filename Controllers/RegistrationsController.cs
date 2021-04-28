using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class RegistrationsController : Controller
    {
        private StateScholarshipEntities db = new StateScholarshipEntities();


        public ActionResult Home()
        {
            return View();
        }


        public ActionResult Successfull()
        {
            return View();
        }


       
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_id,FirstName,LastName,DateofBirth,Gender,Contact,Email,User_category,user_password,FavouritePet,FavouriteMovie,LuckyNumber,approvedByProvider,approvedByInstitution")] Registration registration)
        {

            var checkId = db.Registrations.Where(x => x.User_id == registration.User_id).FirstOrDefault();
            if(checkId==null)
            {
                if (ModelState.IsValid)
                {
                    db.Registrations.Add(registration);
                    db.SaveChanges();
                    return RedirectToAction("Successfull");
                }
            }
            else
            {
                ViewBag.Message = "User Id Already Exists, Please Enter another User ID";
                return View();
            }
            

            return View(registration);
        }

        [HandleError]
        public ActionResult ErrorCheck()
        {
           try
            {
                int a = 10;
                int b = 0;
                int c = a / b;
                return View("Null");
            }
            catch(DivideByZeroException)
            {
                ViewBag.Errormsg = "Check ErrorCheck Controller";
                return View("Error");
            }
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
