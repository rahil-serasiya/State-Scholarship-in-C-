using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Models;

namespace Project.Controllers
{
    public class LoginController : Controller
    {

        private StateScholarshipEntities db;

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            using (db = new StateScholarshipEntities())
            {
                var checkUSer = db.Registrations.Where(d => d.User_id == loginModel.UserId && d.user_password == loginModel.Password && d.User_category == loginModel.UserCategory).FirstOrDefault();

                var userid = db.Registrations.Where(d => d.User_id == loginModel.UserId).FirstOrDefault();
                var password = db.Registrations.Where(d => d.User_id == loginModel.UserId && d.user_password == loginModel.Password).FirstOrDefault();
                var category = db.Registrations.Where(d => d.User_id == loginModel.UserId && d.user_password == loginModel.Password && d.User_category == loginModel.UserCategory).FirstOrDefault();

                if (checkUSer != null)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.UserId.ToString(), false);
                    Session["Name"] = checkUSer.FirstName +" "+ checkUSer.LastName;
                    Session["ID"] = checkUSer.User_id;
                    if (loginModel.UserCategory == "Scholarship Provider")
                    {
                        //return View("ScholarshipProvider");
                        return RedirectToAction("ScholarshipPro", "ScholarshipRegistration");
                    }
                    else
                    {
                        Session["userId"] = checkUSer.User_id.ToString();
                        return RedirectToAction("Student", "ScholarshipRegistration");

                    }
                }
                else
                {
                    if (userid==null)
                    {
                        ViewBag.username = " User Id is not present";
                    }
                    if (password == null)
                    {
                        ViewBag.password = " Password is not matching";
                    }
                    if(category==null)
                    {
                        ViewBag.category = "Select Category";
                    }
                    //ModelState.Clear();
                    ViewBag.RejectMessage = "Invalid user Id or Password";
                    return View("login");

                }

            }

        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            using (db = new StateScholarshipEntities())
            {

                var UserIdCheck = db.Registrations.Where(d => d.User_id == forgotPassword.UserID && d.FavouritePet == forgotPassword.FavouritePet && d.FavouriteMovie == forgotPassword.FavouriteMovie && d.LuckyNumber == forgotPassword.LuckyNumber).FirstOrDefault();
                if (UserIdCheck != null)
                {

                    Session["userid"] = UserIdCheck.User_id.ToString();
                    return RedirectToAction("PasswordReset");
                }
            }
            return View("ForgotPassword");
        }

        [HttpGet]
        public ActionResult PasswordReset()
        {
            if (Session["userid"].ToString() != null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        public ActionResult PasswordReset(PasswordResetModel passwordReset)
        {
            using (db = new StateScholarshipEntities())
            {
                string id = Session["userid"].ToString();

                if (id != null )
                {
                    if (passwordReset.NewPassword != null && passwordReset.ConfirmPassword != null)
                    {
                        int userid = int.Parse(Session["userid"].ToString());

                        var UserIdCheck = db.Registrations.Where(d => d.User_id == userid).FirstOrDefault();

                        UserIdCheck.user_password = passwordReset.NewPassword;

                        db.SaveChanges();

                        ViewBag.SuccessMessage = "Password Updated successful";
                        TempData["SuccessMessage"] = "Password Updated successfully";
                        TempData.Keep();
                        
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

                return RedirectToAction("Login","Login");
            }

        }

        [HttpGet]
        public ActionResult ForgotID()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgotID(ForgotUserID userID)
        {
            using (db = new StateScholarshipEntities())
            {
                var EmailCheck = db.Registrations.Where(d => d.Email == userID.Email && d.FavouritePet == userID.FavouritePet && d.FavouriteMovie == userID.FavouriteMovie && d.LuckyNumber == userID.LuckyNumber).FirstOrDefault();
                if (EmailCheck != null)
                {
                    var id = (from Registrations in db.Registrations
                              where Registrations.Email.Equals(userID.Email)
                              select Registrations.User_id).SingleOrDefault();

                    ViewBag.SuccessMessage = "User Id :" + id;
                }


                else
                {
                    ModelState.Clear();
                    ViewBag.RejectMessage = "Invalid Information given";

                }
                return View();

            }

        }

        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["Name"] = null;
            Session["ID"] = null;


            Session["Password"] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }


        StateScholarshipEntities4 context = new StateScholarshipEntities4();
        public ActionResult HelpCheckRequest(Help help)
        {
            if(Session["userid"]!=null)
            {
                var userid = int.Parse(Session["userId"].ToString());
                return View(context.Helps.Where(x => x.UserId == userid).ToList());
            }
            else
            {
                return View("Error");
            }
            
        }

    }
}