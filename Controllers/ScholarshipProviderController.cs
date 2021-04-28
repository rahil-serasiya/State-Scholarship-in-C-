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
    public class ScholarshipProviderController : Controller
    {
        private StateScholarshipEntities3 db = new StateScholarshipEntities3();

        // GET: ScholarshipProvider
        public ActionResult ViewScholarshipRequest()
        {
            return View(db.scholarshipdetails.Where(x => x.approvedbyScholarshipProvider == "Waiting For Approval").ToList());
        }


        public ActionResult ApprovedScholarshipRequest()
        {
            return View(db.scholarshipdetails.Where(x => x.approvedbyScholarshipProvider =="Approved").ToList());
        }

        // GET: ScholarshipProvider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scholarshipdetail scholarshipdetail = db.scholarshipdetails.Find(id);
            if (scholarshipdetail == null)
            {
                return HttpNotFound();
            }
            return View(scholarshipdetail);
        }

        // GET: ScholarshipProvider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scholarshipdetail scholarshipdetail = db.scholarshipdetails.Find(id);
            if (scholarshipdetail == null)
            {
                return HttpNotFound();
            }
            return View(scholarshipdetail);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_Id,Marks,Grade,DemoCourse,College,approvedbyScholarshipProvider,approvedbyInstitution")] scholarshipdetail scholarshipdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scholarshipdetail).State = EntityState.Modified;
                scholarshipdetail.approvedbyInstitution = "Waiting for Approval";
                db.SaveChanges();
                return RedirectToAction("ViewScholarshipRequest");
            }
            return View(scholarshipdetail);
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
