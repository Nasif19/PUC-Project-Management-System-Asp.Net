using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUC_PMS.Models;
using PUC_PMS.Gateways;
using PUC_PMS.Managers;
using System.Web.Configuration;

namespace PUC_PMS.Controllers
{
    public class LogInController : Controller
    {
        private UserAuthManager UserAuthManager;
        private ProjectListManager ProjectListManager;
        private string masterPassword = WebConfigurationManager.AppSettings["masterPassword"];
        public LogInController()
        {
            UserAuthManager = new UserAuthManager();
            ProjectListManager = new ProjectListManager();
        }
        //User Login
        [HttpGet]
        public ActionResult LogIn(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LogIn logIn)
        {
            if (ModelState.IsValid)
            {
                LogIn user = UserAuthManager.CheckAuthentication(logIn, masterPassword);

                if (user != null)
                {
                   if(user.Type.Equals("Student"))
                   {
                        Session["UserId"] = user.Id;
                        Session["UserName"] = user.UserName;
                        Session["Type"] = user.Type;
                        return RedirectToAction("Index", "StudentProject");
                   }
                   else if(user.Type.Equals("Teacher"))
                   {
                        Session["UserId"] = user.Id;
                        Session["UserName"] = user.UserName;
                        Session["Type"] = user.Type;
                        return RedirectToAction("Index", "ProjectGroup");
                   }
                   else
                   {
                        Session["UserId"] = null;
                        Session["UserName"] = string.Empty;
                        Session["Type"] = string.Empty;
                        return RedirectToAction("LogIn", "Login", new { message = "Invalid your UserName or Password" });
                   }

                }

                else
                {
                    Session["UserId"] = null;
                    Session["UserName"] = string.Empty;
                    Session["Type"] = string.Empty;
                    return RedirectToAction("LogIn", "Login", new { message = "Invalid your UserName or Password" });
                }

            }
            else
            {
                Session["UserId"] = null;
                Session["UserName"] = string.Empty;
                return RedirectToAction("LogIn", "Login", new { message = "Enter your UserName or Password" });
            }
        }

        // for logout
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = string.Empty;
            Session["UserType"] = string.Empty;

            return RedirectToAction("LogIn", "Login");
        }
    }
}