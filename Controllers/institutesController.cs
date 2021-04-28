using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Rotativa;


namespace Project.Controllers
{
    public class institutesController : Controller
    {
        private InstituteRegistrationAndScholarship db = new InstituteRegistrationAndScholarship();
        
        //institute Dashboard
        public ActionResult InstituteDashboard()
        {
            if (Session["InstitutionID"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "institutes");
            }
            
        }

        // Get AddScholarshipDetails
        public ActionResult AddScholarshipDetails()
        {
            if (Session["InstitutionID"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "institutes");
            }
        }

        //Add Scholarship with course Name
        [HttpPost]
        public ActionResult AddScholarshipDetails(Institute_details scholarshipmodel)
        {
            using (db = new InstituteRegistrationAndScholarship())
            {
                if(ModelState.IsValid)
                {
                    db.Institute_details.Add(scholarshipmodel);
                    db.SaveChanges();
                    TempData["ScholarshipAdded"] = "New Scholarship Details Added";
                    TempData.Keep();
                    return RedirectToAction("InstituteDashboard", "institutes");
                }
                else
                {
                    TempData["ScholarshipAdded"] = "Please Fill the details";
                    return View();
                }
                
            }
        }

        

        // GET: institutes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: institutes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationID,InstitutionID,InstitutionName,Address,Ins_Password,Email,Contact,FavoritePet,FavoriteMovie,Luckynumber")] institute institute)
        {
            var institutionIDCheck = db.institutes.Where(x => x.InstitutionID == institute.InstitutionID).FirstOrDefault();
            if (institutionIDCheck==null)
            {
                if (ModelState.IsValid)
                {
                    db.institutes.Add(institute);
                    db.SaveChanges();
                    Session["InstitutionRegistration"] = "Registration Successful Please login...";
                    return RedirectToAction("Login", "institutes");
                }
            }
            else
            {
                ViewBag.InstitutionID = "ID already Exists";
                
            }
            return View(institute);


        }



       
        // GET: institutes/Edit/5
        public ActionResult EditInstitutionDetails(string id)
        {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int cid = int.Parse(id);
                var institute = db.institutes.Where(x => x.InstitutionID == cid).FirstOrDefault();
                if (institute == null)
                {
                    return HttpNotFound();
                }
                return View(institute);
        }


        // Post: institutes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInstitutionDetails( institute institute)
        {
        
            
            if (ModelState.IsValid)
            {
                db.Entry(institute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("InstituteDashboard");
            }
            else
            {
                return View("Error");
            }
           
        }
        
        //Login - institute
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public int ID;
        [HttpPost]
        public ActionResult Login(InstituteLogin insloginModel)
        {
            using (db = new InstituteRegistrationAndScholarship())
            {
                var checkInstitution = db.institutes.Where(d => d.InstitutionID == insloginModel.InstitutionID && d.Ins_Password == insloginModel.Password).FirstOrDefault();

                var Institutionid = db.institutes.Where(d => d.InstitutionID == insloginModel.InstitutionID).FirstOrDefault();
                var password = db.institutes.Where(d => d.InstitutionID == insloginModel.InstitutionID && d.Ins_Password == insloginModel.Password).FirstOrDefault();
                

                if (checkInstitution != null)
                {
                    ID = insloginModel.InstitutionID;
                    Session["InstitutionName"] = checkInstitution.InstitutionName;
                    Session["InstitutionID"] = checkInstitution.InstitutionID.ToString();
                    Session["IDpass"] = checkInstitution.InstitutionID;
                    TempData["InstitutionID"] = checkInstitution.InstitutionID;
                    TempData.Keep();

                    Session["InstitutionIDTOPASS"] = (int) checkInstitution.InstitutionID;
                    
                    return RedirectToAction("InstituteDashboard", "institutes");

                    
                }
                else
                {
                    if (Institutionid == null)
                    {
                        ViewBag.username = " User Id is not present";
                    }
                    if (password == null)
                    {
                        ViewBag.password = " Password is not matching";
                    }
                    
                    //ModelState.Clear();
                    ViewBag.RejectMessage = "Invalid user Id or Password";
                    return View("Login");

                }

            }

        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(InstituteForgetPassword forgotPassword)
        {
            using (db = new InstituteRegistrationAndScholarship())
            {

                var InstituteIdCheck = db.institutes.Where(d => d.InstitutionID == forgotPassword.InstitutionID && d.FavoritePet == forgotPassword.FavouritePet && d.FavoriteMovie == forgotPassword.FavouriteMovie && d.Luckynumber == forgotPassword.LuckyNumber).FirstOrDefault();
                if (InstituteIdCheck != null)
                {

                    Session["InstitutionID"] = InstituteIdCheck.InstitutionID.ToString();
                    return RedirectToAction("PasswordReset", "institutes");
                }
            }
            return View("ForgotPassword","institutes");
        }


        [HttpGet]
        public ActionResult PasswordReset()
        {
            if (Session["InstitutionID"].ToString() != null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        public ActionResult PasswordReset(InstitutePasswordReset passwordReset)
        {
            using (db = new InstituteRegistrationAndScholarship())
            {
                string id = Session["InstitutionID"].ToString();

                if (id != null)
                {
                    if (passwordReset.NewPassword != null && passwordReset.ConfirmPassword != null)
                    {
                        int InstituteID = int.Parse(Session["InstitutionID"].ToString());

                        var InstituteIdCheck = db.institutes.Where(d => d.InstitutionID == InstituteID).FirstOrDefault();

                        InstituteIdCheck.Ins_Password = passwordReset.NewPassword;

                        db.SaveChanges();

                        ViewBag.SuccessMessage = "Password Updated successful";
                    }

                    else
                    {
                        ViewBag.Message = "Please enter Details";
                    }

                }

                else
                {
                    return RedirectToAction("Login");
                }

                return View();
            }

        }

        [HttpGet]
        public ActionResult ForgotID()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgotID(ForgotInstituteID userID)
        {
            using (db = new InstituteRegistrationAndScholarship())
            {
                var EmailCheck = db.institutes.Where(d => d.Email == userID.Email && d.FavoritePet == userID.FavouritePet && d.FavoriteMovie == userID.FavouriteMovie && d.Luckynumber == userID.LuckyNumber).FirstOrDefault();
                if (EmailCheck != null)
                {
                    var id = (from institute in db.institutes
                              where institute.Email.Equals(userID.Email)
                              select institute.InstitutionID).SingleOrDefault();

                    ViewBag.SuccessMessage = "Institute Id :" + id;
                }


                else
                {
                    ModelState.Clear();
                    ViewBag.RejectMessage = "Invalid Information given";

                }
                return View();

            }

        }

        
        public ActionResult Approvescholarship()
        {
             int InstituteID = int.Parse(Session["InstitutionID"].ToString());
            //InstituteID = int.Parse(id);
            return View(db.ScholarshipRequests.Where(x=>x.institutionId==InstituteID).ToList());
        }

        public ActionResult EditRequest(int id)
        {
            return View(db.ScholarshipRequests.Where(x=>x.Requestid==id).FirstOrDefault());
        }
        

        public ActionResult RequestDetials(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScholarshipRequest Scholarship_request  = db.ScholarshipRequests.Find(id);
            if (Scholarship_request == null)
            {
                return HttpNotFound();
            }
            return View(Scholarship_request);
        }

        //Approve 
        [HttpPost]
        public ActionResult EditRequest(ScholarshipRequest scholarship)
        {
           
            scholarship.DateofApproval = DateTime.Now;
            db.Entry(scholarship).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Approvescholarship","institutes");
            
        }

        public ActionResult InstituteScholarshipStatus(string id)
        {
            int studentId = int.Parse(id);
            return View(db.ScholarshipRequests.Where(x => x.StudentId == studentId).ToList());
        }

        /// <summary>
        /// ScholarShip Details 
        /// </summary>
        /// <returns></returns>
        /// 
        
        public ActionResult ScholarshipDetailsYearWise(int reportid)
        {

            var ScholarshipList = db.Institute_details.Where(x => x.institutionID == reportid).ToList();

            //InstituteID = int.Parse(id);
            return View(ScholarshipList);
        }


       

        public ActionResult PrintAll()
        {
            int InstituteID = int.Parse(Session["InstitutionID"].ToString());
            var q = new ActionAsPdf("ScholarshipDetailsYearWise",new { reportid = InstituteID });
            return q;

        }



        /// <summary>
        /// Scholarship Request
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult ScholarshipRequests(int reportid,int requestyear)
        {

            var ScholarshipList = db.ScholarshipRequests.Where(x => x.institutionId == reportid && x.DateofApproval.Year== requestyear).ToList();

            //InstituteID = int.Parse(id);
            return View(ScholarshipList);
        }

        public ActionResult PrintAllRequest()
        {
            int requestyear = int.Parse(TempData["year"].ToString());
            int InstituteID = int.Parse(Session["InstitutionID"].ToString());
            var q = new ActionAsPdf("ScholarshipRequests", new { reportid = InstituteID , requestyear= requestyear });
            return q;

        }
        /// <summary>
        /// Availing Students Reports
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult AvailingStudents( int reportid)
        {
            var ScholarshipList = db.ScholarshipRequests.Where(x => x.institutionId == reportid && x.ApprovedByInstitution== "Approved").ToList();
            return View(ScholarshipList);
        }

        public ActionResult PrintAllApproved()
        {
            int InstituteID = int.Parse(Session["InstitutionID"].ToString());
            var ApprovedStudents = new ActionAsPdf("AvailingStudents", new { reportid = InstituteID });
            return ApprovedStudents;
        }


        /// <summary>
        /// Redirect Methods
        /// </summary>
        /// <returns></returns>
        public ActionResult YearWiseReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YearWiseReport(string id, Report report)
        {
            string category = report.category;  

            int year = report.Year;
            TempData["year"] = year;

            int inID = int.Parse(id);
            if (category == "Scholarship Details")
            {
                return RedirectToAction("ScholarshipDetailsYearWise", "institutes", new { reportid = (int) inID });
            }
            else if (category == "Scholarship Request")
            {
                return RedirectToAction("ScholarshipRequests", "institutes", new { reportid = (int)inID, requestyear=year });
            }
            else if(category== "Availing Students")
            {
                return RedirectToAction("AvailingStudents", "institutes", new { reportid = (int)inID, requestyear = year });
            }
            return View();
        }

    }
}
