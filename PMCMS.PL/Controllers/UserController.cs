using BCrypt.Net;
using PMCMS.BLL.Repos;
using PMCMS.BLL.Utility;
using PMCMS.BSL.Authorization;
using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.PL.Controllers
{
    public class UserController : Controller
    {
        UserRepo ur = new UserRepo();
        // GET: User
        [UserAuth]
        public ActionResult Edit()
        {
            User currentUser = Session["user"] as User;
            return View(currentUser);
        }

        [UserAuth, HttpPost]
        public ActionResult Edit(User user)
        {
            ApiResponse response = new ApiResponse();
            User current = Session["user"] as User;

            current.FirstName = user.FirstName;
            current.LastName = user.LastName;
            current.Email = user.Email;

            response.Status = ur.UpdateUser(current, out string result);
            response.Result = result;

            return View(current);
        }

        [UserAuth, HttpPost]
        public JsonResult Pass(string oldPass, string newPass, string newPassRe)
        {
            ApiResponse response = new ApiResponse();
            User current = Session["user"] as User;

            if(newPass != newPassRe)
            {
                response.Status = false;
                response.Result = "Girdiğiniz yeni parolalar eşit değil, tekrar deneyiniz.";
                return Json(response);
            }

            if(!BCrypt.Net.BCrypt.Verify(oldPass, current.Password))
            {
                response.Status = false;
                response.Result = "Eski parolanız doğrulanamadı. Lütfen tekrar deneyiniz.";
                return Json(response);
            }

            response.Status = ur.UpdatePass(newPass, current.UserID, out string result);
            response.Result = result;
            
            return Json(response);
        }
    }
}