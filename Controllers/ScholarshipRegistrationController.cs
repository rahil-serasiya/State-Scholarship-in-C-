using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ScholarshipRegistrationController : Controller
    {
        StateScholarshipEntities3 db;

        public ScholarshipRegistrationController()
        {
            db = new StateScholarshipEntities3();
        }
        // GET: Registration
        public ActionResult AddStudent()
        {
            if(Session["userid"] != null)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
            
        }
        [HttpPost]
        public ActionResult AddStudent(scholarshipdetail model)
        {
                var checkId = db.scholarshipdetails.Where(x => x.student_Id == model.student_Id).FirstOrDefault();
            
                if (checkId == null && Session["userid"]!=null)
                {
                    if (ModelState.IsValid)
                    {

                        model.approvedbyScholarshipProvider = "Waiting For Approval";
                        model.approvedbyInstitution = "Waiting For Approval";
                    db.scholarshipdetails.Add(model);

                        db.SaveChanges();
                        ViewBag.SuccessMessage = "Your Request to apply for Scholarship is submitted successfully";
                        return View("Student"); 

                    }
                }
                else
                {
                    ViewBag.Message = "You have already made a Request for Scholarship wait for confirmation by Scholarship Provider";
                    return View();
                }
                ModelState.Clear();
                return RedirectToAction("Student");


        }

        public ActionResult ScholarshipPro()
        {
            if(Session["ID"] !=null)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
            
        }

        public ActionResult Student()
        {
            if(Session["userid"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Login");
            }
           
        }

        public ActionResult ApplyForInstituionScholarship()
        {
            try
            {
                StateScholarshipEntities3 dbscholarshipproviderdb = new StateScholarshipEntities3();
                int studentID = int.Parse(Session["ID"].ToString());
                if (studentID != 0)
                {
                
                    InstituteRegistrationAndScholarship dbscholarprovider;
                    dbscholarprovider = new InstituteRegistrationAndScholarship();
                    return View(dbscholarprovider.Institute_details.ToList());
                    
                }
                else
                {
                    return View("Error");
                }


            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        public ActionResult ScholarshipDetails(int sid)
        {
            InstituteRegistrationAndScholarship dbscholarprovider= new InstituteRegistrationAndScholarship();
            return View(dbscholarprovider.SchlorshipCriterias.Where(x=>x.ScholarshipID==sid).FirstOrDefault());
        }

        public ActionResult ApplyScholarship(int ScholarshipID,int InstituteID,string ScholarshipName,int ScholarshipAmount,string Course)
        {
            InstituteRegistrationAndScholarship dbscholarprovider = new InstituteRegistrationAndScholarship();
            ScholarshipRequest sr = new ScholarshipRequest();
            sr.ScholarshipID = ScholarshipID;
            sr.institutionId = InstituteID;
            var institutionDetails = dbscholarprovider.institutes.Where(x => x.InstitutionID == InstituteID).FirstOrDefault();
            sr.institutionName = institutionDetails.InstitutionName;
            sr.ScholarshipName = ScholarshipName;
            sr.ScholarshipAmount = ScholarshipAmount;
            sr.Course = Course;
            sr.DateofApproval = DateTime.Now;
            sr.StudentId = int.Parse(Session["userid"].ToString());
            sr.StudentName = Session["Name"].ToString();

            return View(sr);
        }

        [HttpPost]
        public ActionResult ApplyScholarship(ScholarshipRequest scholarshiprequest)
        {
            InstituteRegistrationAndScholarship dbscholarprovider = new InstituteRegistrationAndScholarship();
            scholarshiprequest.ApprovedByInstitution = "Waiting For Approval";
            var isEligible = dbscholarprovider.SchlorshipCriterias.Where(x => x.ScholarshipID == scholarshiprequest.ScholarshipID).FirstOrDefault();

            if(scholarshiprequest.Score>=isEligible.PercentageMerit && scholarshiprequest.AnnualIncome<=isEligible.AnnualIncome)
            {
                var courseCheck = dbscholarprovider.ScholarshipRequests.Where(x => x.StudentId == scholarshiprequest.StudentId && x.ScholarshipName == scholarshiprequest.ScholarshipName).FirstOrDefault();
                if(courseCheck==null)
                {
                    dbscholarprovider.ScholarshipRequests.Add(scholarshiprequest);
                    dbscholarprovider.SaveChanges();
                    TempData["Eligible"] = "Congartulations You are eligible and Your request is Submited Successfully" +
                        " Please wait for the Approval";
                    return RedirectToAction("Student", "ScholarshipRegistration");
                }
                else
                {
                    ViewBag.message = "You have Already Applied for this Scholarship";
                    return View();
                } 

            }
            else
            {
                TempData["Eligible"] = "Sorry Your Criteria Does not meet the Requirements";
                return RedirectToAction("Student", "ScholarshipRegistration");
            }        
        }

    }
}