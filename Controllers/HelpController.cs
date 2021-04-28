using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class HelpController : Controller
    {
        //// GET: Help
        //public ActionResult Index()
        //{
        //    return View();
        //}

        StateScholarshipEntities4 db;
        // GET: Help
        public ActionResult Index()
        {
            using (db = new StateScholarshipEntities4())
            {
                return View(db.Helps.Where(x => x.Solved == "Work in Progress").ToList());
            }
        }

        [HttpGet]
        public ActionResult HelpForm()
        {
            Help h = new Help();
            h.DateOfTicket = DateTime.Now;
            h.Resolution = "In progress";
            h.Solved = "Work in Progress";
            return View(h);

        }

        [HttpPost]
        public ActionResult HelpForm(Help model)
        {
            if (ModelState.IsValid)
            {
                using (db = new StateScholarshipEntities4())
                {
                    model.DateOfTicket = DateTime.Now;
                    model.Resolution = "In progress";
                    model.Solved = "Work in Progress";
                    db.Helps.Add(model);
                    db.SaveChanges();
                    //ModelState.Clear();
                    ViewBag.SuccessMessage = "Issue successfully submitted";
                    return View();
                }
            }

            else
            {
                ModelState.Clear();
                ViewBag.RejectMessage = "Please Fill the Details";
                return View();
            }

        }



        public ActionResult Details(int id)
        {

            using (db = new StateScholarshipEntities4())
            {
                return View(db.Helps.Where(x => x.RequestId == id).FirstOrDefault());
            }


        }

        /*
        public ActionResult RequestView(string id)
        {
            var userId = int.Parse(id);
            return View(db.Helps.Where(x => x.UserId == userId).FirstOrDefault());
        }
        */

        public ActionResult Edit(int id)
        {
            using (db = new StateScholarshipEntities4())
            {

                return View(db.Helps.Where(x => x.RequestId == id).FirstOrDefault());
            }
        }

        // POST: InstitutionScholorship/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Help help)
        {
            try
            {
                // TODO: Add update logic here
                using (db = new StateScholarshipEntities4())
                {
                  
                    db.Entry(help).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    
    }
}