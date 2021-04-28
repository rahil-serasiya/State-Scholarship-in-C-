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
    public class EditCourseController : Controller
    {
        private InstituteRegistrationAndScholarship db = new InstituteRegistrationAndScholarship();

        // GET: EditCourse
        int InstitutionID;
        public ActionResult Index(string id)
        {
            int cid = int.Parse(id);
            InstitutionID=cid;
            return View(db.Institute_details.Where(x=>x.institutionID==cid).ToList());
        }

        // GET: EditCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institute_details institute_details = db.Institute_details.Find(id);
            if (institute_details == null)
            {
                return HttpNotFound();
            }
            return View(institute_details);
        }

        // GET: EditCourse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditCourse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScholarshipID,institutionID,CourseName,ScholarshipName,ScholarshipAmount")] Institute_details institute_details)
        {
            if (ModelState.IsValid)
            {
                db.Institute_details.Add(institute_details);
                db.SaveChanges();
                return RedirectToAction("Index", InstitutionID);
            }

            return View(institute_details);
        }

        // GET: EditCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institute_details institute_details = db.Institute_details.Find(id);
            if (institute_details == null)
            {
                return HttpNotFound();
            }
            return View(institute_details);
        }

        // POST: EditCourse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScholarshipID,institutionID,CourseName,ScholarshipName,ScholarshipAmount")] Institute_details institute_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institute_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("InstituteDashboard","institutes");
            }
            return View(institute_details);
        }

        // GET: EditCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institute_details institute_details = db.Institute_details.Find(id);
            if (institute_details == null)
            {
                return HttpNotFound();
            }
            return View(institute_details);
        }

        // POST: EditCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Institute_details institute_details = db.Institute_details.Find(id);
            //db.Institute_details.Remove(institute_details);
            //db.SaveChanges();
            //return RedirectToAction("InstituteDashboard","institutes");

            var institute_details = db.Institute_details.Where(x=>x.ScholarshipID==id).FirstOrDefault();
            db.Institute_details.Remove(institute_details);
            var scholarshipCriteria = db.SchlorshipCriterias.Where(x => x.ScholarshipID == id).FirstOrDefault();
            db.SchlorshipCriterias.Remove(scholarshipCriteria);
            db.SaveChanges();
            return RedirectToAction("InstituteDashboard","institutes");

            
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
