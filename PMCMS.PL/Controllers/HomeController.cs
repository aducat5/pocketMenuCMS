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
    public class HomeController : Controller
    {
        private readonly SellerRepo sr = new SellerRepo();
        [UserAuth]
        public ActionResult Dashboard()
        {
            User currentUser = Session["user"] as User;
            List<Seller> sellersOfUser = sr.GetSellersOfUser(currentUser.UserID);
            if (sellersOfUser.Count > 0)
                return View(sellersOfUser);
            else
                return RedirectToAction("New", "Restaurant");

        }

        public ActionResult Landing() => View(); 
        public ActionResult NotFound() => View();
    }
}