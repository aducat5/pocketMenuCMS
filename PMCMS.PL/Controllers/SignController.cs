using PMCMS.BLL.Repos;
using PMCMS.BSL.Authorization;
using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.PL.Controllers
{
    public class SignController : Controller
    {
        private readonly UserRepo ur = new UserRepo();

        [NonUserAuth]
        public ActionResult Up() => View();

        [NonUserAuth, HttpPost]
        public ActionResult Up(User user)
        {
            bool sonuc = ur.InsertUser(user, out string islemSonucu);
            if (sonuc)
            {
                return RedirectToAction("In", new { regState = "suc" });
            }
            else
            {
                ViewBag.RegState = islemSonucu;
                ViewBag.AlertState = "alert alert-danger";
                return View();
            }
        }

        [NonUserAuth]
        public ActionResult In() => View();

        [NonUserAuth, HttpPost]
        public ActionResult In(string email, string password)
        {
            try
            {
                User user = ur.GetUser(email, password);
                
                string requesResult = "Username or password is wrong. Please try again.";

                if (user != null)
                {
                    Session["user"] = user;
                    ViewBag.LoginNote = "Welcome, " + user.FirstName + " " + user.LastName;
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ViewBag.LoginNote = requesResult;
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [NonUserAuth]
        public ActionResult Recover() => View();

        [UserAuth]
        public ActionResult LogOut()
        {
            //TODO: Say drop session to api
            Session["user"] = null;
            return RedirectToAction("In");
        }

    }
}