using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_and_SignUP.Models;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace Login_and_SignUP.Controllers
{
    public class UsersController : Controller
    {
        UsersDB DB = new UsersDB();
        /*===============================================================
         * Index Action
         * ==============================================================*/
        public ActionResult Index()
        {
            if(Request.Cookies["userCookie"] != null)
            {
                return RedirectToAction("HomePage");
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }
        /*===============================================================
         * Registration Get Action
         * ==============================================================*/
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        /*===============================================================
        * Registration Post Action
        * ==============================================================*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Include = "FirstName,LastName,UserName,EmailID,Password,DateOfBirth")]User U)
        {
            if (ModelState.IsValid)
            {
                var EmailExist = IsEmailExist(U.EmailID);
                if (EmailExist)
                {
                    ModelState.AddModelError("EmailExist", "Email is aleardy Exist");
                    //@ViewBag.EmailExistMessage =  "Email is aleardy Exist"
                    return View();
                }
                //1.2 generate ActivationCode
                U.ActivationCode = Guid.NewGuid();

                //1.3 Hashing Password 
                U.Password = Crypto.Hash(U.Password);
                //U.ConfirmPassword = Crypto.Hash(U.ConfirmPassword);

                //1.4
                U.IsEmailVerified = false;

                //1.5 save data in the database
                DB.Database.ExecuteSqlCommand(@"insert into users (FirstName,LastName,UserName,EmailID,Password,DateOfBirth,IsEmailVerified,ActivationCode) Values (@firstName,@lastName,@userName,@email,@password, @birthDay,@isEmailVerified,@activationCode) ", new SqlParameter("firstName", U.FirstName), new SqlParameter("lastName", U.LastName), new SqlParameter("userName", U.UserName), new SqlParameter("email", U.EmailID), new SqlParameter("password", U.Password), new SqlParameter("birthDay", U.DateOfBirth), new SqlParameter("isEmailVerified", U.IsEmailVerified), new SqlParameter("activationCode", U.ActivationCode));
                DB.SaveChanges();

                //1.6 send verfication email
                SendValidationLinkEmail(U.EmailID, U.ActivationCode.ToString());

                return RedirectToAction("Congratolation");

            }
            else
            {
                ViewBag.InvalidMessage = " invalid model or invalid request";
                return View();
            }
        }
        /*===============================================================
        * active  Email
        * ==============================================================*/
        public ActionResult VerifyAccount(string id)
        {
            if(RouteData.Values["id"] != null)
            {
                int NumActivationCode = DB.Database.SqlQuery<int>(@"SELECT Id FROM users Where ActivationCode = @id", new SqlParameter("id", id)).Count();
                if (NumActivationCode <=  0 )
                {
                    ViewBag.ErrorMessage = "This is A fake ActivationCode,, Register Faild";
                    ViewBag.WelcomeMessage = null;
                }
                else
                {
                    ViewBag.WelcomeMessage = "Congratolations,, Email is active now ";
                    ViewBag.ErrorMessage = null;
                    int IsEmailVerified =  DB.Database.ExecuteSqlCommand(@"UPDATE USERS SET IsEmailVerified = 1 Where ActivationCode = @id", new SqlParameter("id", id));
                }
            }
            return View();
        }
        /*===============================================================
        * Congratolation Action
        * ==============================================================*/
        public ActionResult Congratolation()
        {
            return View();
        }
        /*===============================================================
        * Login Get Action
        * ==============================================================*/
        public ActionResult Login()
        {
            return View();
        }
        /*===============================================================
        * Login Post  Action
        * ==============================================================*/
        [HttpPost]
        public ActionResult Login([Bind(Include = "EmailID,Password")] UserLogin userLogin)
        {
            if(ModelState.IsValid)
            {
                var EmailExist = IsEmailExist(userLogin.EmailID);
                if (EmailExist == false)
                {
                    ModelState.AddModelError("EmailIsNotExist", "Email is Not Exist");
                    //@ViewBag.EmailExistMessage =  "Email is aleardy Exist"
                    return View();
                }
                else
                {
                    if(IsPasswordCorrect(Crypto.Hash(userLogin.Password)) == false)
                    {
                        ModelState.AddModelError("PasswordIsNotCorrect", "Password Is NotCorrect");
                        return View();
                    }
                    else
                    {
                        //Get data of user from database
                        User MyUser = DB.Users.SqlQuery(@"SELECT * FROM USERS WHERE EmailID = @email And Password = @pass", new SqlParameter("email", userLogin.EmailID), new SqlParameter("pass", Crypto.Hash(userLogin.Password))).Single<User>();
                        //create cookie
                        HttpCookie UserCookie = new HttpCookie("userCookie");
                       
                            
                            UserCookie["firstName"] = MyUser.FirstName;
                            UserCookie["lastName"] = MyUser.LastName;
                            UserCookie["UserName"] = MyUser.UserName;
                            UserCookie["email"] = MyUser.EmailID;
                            UserCookie["password"] = MyUser.Password;
                            UserCookie["birthDay"] = MyUser.DateOfBirth.ToString();
                            UserCookie["ActivationCode"] = MyUser.ActivationCode.ToString();
                            UserCookie["isEmailActive"] = MyUser.IsEmailVerified.ToString();
                            UserCookie.Expires = DateTime.Now.AddMinutes(3);
                        
                        //add cookie
                        Response.Cookies.Add(UserCookie);
                        return RedirectToAction("HomePage");
                    }
                }
            }
            else
            {
                ViewBag.InvalidRequest = " invalid model or invalid request ";
            }
            return View();
        }
        /*===============================================================
        * HomePage Action
        * ==============================================================*/
        public ActionResult HomePage()
        {
            //server request the cookie file from the user computer to use the values of varibles which stores in it 
            var MyCookie = Request.Cookies["userCookie"];
            if(MyCookie != null)
            {
                ViewBag.firstName = MyCookie["firstName"];
                ViewBag.lastName = MyCookie["lastNamee"];
                ViewBag.userName = MyCookie["UserName"];
                ViewBag.email = MyCookie["email"];
                ViewBag.password = MyCookie["password"];
                ViewBag.birthDay = MyCookie["birthDay"];
                ViewBag.ActivationCode = MyCookie["ActivationCode"];
                ViewBag.isEmailActive = MyCookie["isEmailActive"];
            }
            else
            {
               return RedirectToAction("CookieExpired");
            }
           
         
            return View();
        }
        /*===============================================================
       * CookieExpired
       * ==============================================================*/
        public ActionResult CookieExpired()
        {
            return View();
        }
        /*===============================================================
        * logout
        * ==============================================================*/
        public ActionResult Logout()
        {
            if (Request.Cookies["userCookie"] != null)
            {
                HttpCookie myCookie = new HttpCookie("userCookie");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Login"); 
        }
        /*===============================================================
        * IsEmailExist
        * ==============================================================*/
        [NonAction] 
        public bool IsEmailExist(string Email)
        {
            int IsEmailExitOrNot = DB.Database.SqlQuery<string>(@"SELECT EmailID FROM users Where EmailID = @email", new SqlParameter("email", Email)).Count();
            if (IsEmailExitOrNot <= 0)
                return false;
            else
                return true;
        }
        /*===============================================================
        * IsPasswordCorrect
        * ==============================================================*/
        [NonAction]
        public bool IsPasswordCorrect(string password)
        {
            int IsPasswordCorrectOrNot = DB.Database.SqlQuery<string>(@"SELECT Password FROM users Where Password = @pass", new SqlParameter("pass", password)).Count();
            if (IsPasswordCorrectOrNot <= 0)
                return false;
            else
                return true;
        }
        /*===============================================================
        * SendValidationLinkEmail
        * ==============================================================*/
        [NonAction]
        public void SendValidationLinkEmail(string Email,string ActivationCode)
        {
            string VerifyURL = "/Users/VerifyAccount/" + ActivationCode;
            var Link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, VerifyURL);
            /**********************************
             * enter your email web application
             **********************************/
            var FromEmail = new MailAddress("youremail", "your name");
            /**********************************
             * enter your password
             **********************************/
            var FromEmailPassword = "youpassword";
            var ToEmail = new MailAddress(Email);
            string subject = "YoUr Account is created Successfully";
            string body = "<br/><br> we are happy to tell you that YoUr Account is created Successfully..<BR/>please click the link below to confirm your account : <a href='" + Link + " '>" + Link + "</a>";

            var Smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail.Address , FromEmailPassword)
            };

            var message = new MailMessage(FromEmail, ToEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            };

            Smtp.Send(message);
        }
        

    }
}