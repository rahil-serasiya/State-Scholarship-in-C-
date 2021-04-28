using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AddScholarshipCriteriaController : Controller
    {
        
        
        InstituteRegistrationAndScholarship db;
        // GET: InstitutionScholorship
        
            public ActionResult DisplayScholorship()
            {
                using (db = new InstituteRegistrationAndScholarship())
                {
                    int id = int.Parse(Session["InstitutionID"].ToString());
                    return View(db.SchlorshipCriterias.Where(x=>x.InstituteId==id).ToList());
                }

            }



            // GET: InstitutionScholorship/Details/5
            public ActionResult Details(int id)
            {
                using (db = new InstituteRegistrationAndScholarship())
                {

                    return View(db.SchlorshipCriterias.Where(x => x.id == id).FirstOrDefault());
                }

            }



            // GET: InstitutionScholorship/Create
            public ActionResult Create(int sid , int insID, string ScholarName)
                                                            
            {
                InstituteRegistrationAndScholarship dbins = new InstituteRegistrationAndScholarship();
                SchlorshipCriteria SC = new SchlorshipCriteria();
                SC.ScholarshipID = sid;
                SC.InstituteId = insID;
               
                SC.ScholarshipName = ScholarName;
                
              
                return View(SC);
            }



            // POST: InstitutionScholorship/Create
            [HttpPost]
            public ActionResult Create(SchlorshipCriteria model)
            {
                try
                {// TODO: Add insert logic here
                    using (db = new InstituteRegistrationAndScholarship())
                    {
                        
                        db.SchlorshipCriterias.Add(model);  
                        db.SaveChanges();
                        TempData["AddCriteria"] = "Criteria Added Successfully";
                        TempData.Keep();
                        return RedirectToAction("DisplayScholorship", "AddScholarshipCriteria");
                    }
                }
                catch
                {
                    ViewBag.Added = "Criteria Already Added Proceed with Update / Check criteria";
                    return View();
                }
            }



            // GET: InstitutionScholorship/Edit/5
            public ActionResult Edit(int id)
            {
                using (db = new InstituteRegistrationAndScholarship())
                {
                    return View(db.SchlorshipCriterias.Where(x => x.id == id).FirstOrDefault());
                }
            }



            // POST: InstitutionScholorship/Edit/5
            [HttpPost]
            public ActionResult Edit(int id, SchlorshipCriteria criteria)
            {
                try
                {
                    // TODO: Add update logic here
                    using (db = new InstituteRegistrationAndScholarship())
                    {
                        db.Entry(criteria).State = EntityState.Modified;
                        db.SaveChanges();
                    }



                    return RedirectToAction("DisplayScholorship");
                }
                catch
                {
                    return View();
                }
            }




        
    }
}