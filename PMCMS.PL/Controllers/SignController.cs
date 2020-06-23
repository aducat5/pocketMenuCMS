using PMCMS.BSL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.PL.Controllers
{
    public class SignController : Controller
    {
        [NonUserAuth]
        public ActionResult Up() => View();

        [NonUserAuth, HttpPost]
        public ActionResult Up(string email, string password)
        {
            try
            {
                //TODO: Validate email and password
                //TODO: Do login on api here
                //If successfull

                User user = new User();
                string requesResult = "Username or password is wrong. Please try again.";

                if (user != null)
                {
                    Session["user"] = user;
                    ViewBag["LoginNote"] = "Welcome, " + user.FirstName + " " + user.LastName;
                    return RedirectToAction("Dashoard", "Home");
                }
                else
                {
                    ViewBag["LoginNote"] = requesResult;
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public ActionResult In()
        {
            return View();
        }
        public ActionResult Recover()
        {
            return View();
        }

    }
}