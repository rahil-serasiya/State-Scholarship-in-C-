using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Rotativa;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        StateScholarshipEntities1 db;
        public AdminController()
        {
            db = new StateScholarshipEntities1();
        }
        // GET: Admin
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminCred model)
        {
            /*
            var credentialCheck = db.AdminCreds.Where(x => x.Admin_Username.Equals(model.Admin_Username, 
                StringComparison.InvariantCultureIgnoreCase) &&
                 x.Admin_Password == model.Admin_Password).FirstOrDefault();
            */

            var username = db.AdminCreds.Select(s => s.Admin_Username).First();
            var passsword = db.AdminCreds.Select(s => s.Admin_Password).First();


            if (ModelState.IsValid)
            {
                if (model.Admin_Username == username && model.Admin_Password == passsword)
                {
                    Session["ID"] = username;
                    Session["Password"] = passsword;
                   
                   

                    return RedirectToAction("AdminPortal");
                }
                else
                {
                    if (model.Admin_Username != username)
                    {
                        ViewBag.username = " Admin Username is not present";
                    }
                    if (model.Admin_Password != passsword)
                    {
                        ViewBag.password = " Password is not matching";
                    }
                }
            }
            return View();
        }

        public ActionResult AdminPortal()
        {
            if (Session["ID"] != null && Session["Password"] != null)
            {
                StateScholarshipEntities4 con = new StateScholarshipEntities4();
                var count = con.Helps.Count(x => x.Solved == "Work in Progress");

                ViewBag.Data = count;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        /// <summary>
        /// Institution Scholarship Provided
        /// </summary>
        /// <returns></returns>
        /// 

        private InstituteRegistrationAndScholarship dbreport = new InstituteRegistrationAndScholarship();
        public ActionResult InstitutionScholarshipProvided()
        {
            return View(dbreport.SchlorshipCriterias.ToList());
        }

        public ActionResult PrintAllScholarship()
        {
            
            var ApprovedStudents = new ActionAsPdf("InstitutionScholarshipProvided");
            return ApprovedStudents;
        }

        /// <summary>
        /// All Students Awailig Scholarship
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult StudentAvailingScholarshipInstitution()
        {
            var ScholarshipList = dbreport.ScholarshipRequests.Where(x=> x.ApprovedByInstitution == "Approved").ToList();
            return View(ScholarshipList);
        }

        public ActionResult PrintAllApproved()
        { 
            var ApprovedStudents = new ActionAsPdf("StudentAvailingScholarshipInstitution");
            return ApprovedStudents;
        }

        /// <summary>
        /// ScholarsShip Provider list
        /// </summary>
        /// <returns></returns>
        /// 
        StateScholarshipEntities ngodb = new StateScholarshipEntities();


        public ActionResult ScholarshipProviderNGO()
        {
            var ScholarshipList = ngodb.Registrations.Where(x => x.User_category == "Scholarship Provider").ToList();
            return View(ScholarshipList);
        }

        public ActionResult PrintAllNGO()
        {
            var ApprovedStudents = new ActionAsPdf("ScholarshipProviderNGO");
            return ApprovedStudents;
        }


        /// <summary>
        /// All the Students Awailing the Scholarship
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult StudentAwaitingforScholarship()
        {
            var AwaitingStudents = dbreport.ScholarshipRequests.Where(x => x.ApprovedByInstitution == "Waiting For Approval").ToList();
            return View(AwaitingStudents);
        }

        public ActionResult PrintAllAwaitingStudents()
        {
            var ApprovedStudents = new ActionAsPdf("StudentAwaitingforScholarship");
            return ApprovedStudents;
        }

        /// <summary>
        /// All the students Benefitted from the NGO i.e ScholarShip Provider
        /// </summary>

        StateScholarshipEntities3 ngobenefitted = new StateScholarshipEntities3();

        public ActionResult StudentBenefitedNGO()
        {
            var NGOStudents = ngobenefitted.scholarshipdetails.Where(x => x.approvedbyScholarshipProvider == "Approved").ToList();
            return View(NGOStudents);
        }


        public ActionResult AllBenefittedStudentsNGO()
        {
            var ApprovedStudents = new ActionAsPdf("StudentBenefitedNGO");
            return ApprovedStudents;
        }



        /// <summary>
        /// repport redirection
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult YearWiseReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YearWiseReport(AdminReport report)
        {
            string category = report.category;

            int year = report.year;
            TempData["year"] = year;
           
            
            if (category == "Institution Scholarship Provided")
            {
                return RedirectToAction("InstitutionScholarshipProvided", "Admin");
            }
            else if (category == "Student Availing Scholarship Institution")
            {
                return RedirectToAction("StudentAvailingScholarshipInstitution", "Admin");
            }
            else if (category == "Scholarship Provider(NGO)")
            {
                return RedirectToAction("ScholarshipProviderNGO", "Admin");
            }
            else if (category == "Student Awaiting for Scholarship")
            {
                return RedirectToAction("StudentAwaitingforScholarship", "Admin");
            }
            else if(category == "Student Benefited (NGO)")
            {
                return RedirectToAction("StudentBenefitedNGO", "Admin");
            }
            return View();
        }


    }
}