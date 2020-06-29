using PMCMS.BLL.Repos;
using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.PL.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult All()
        {
            return View();
        }

        public ActionResult Display(int id)
        {
            SellerRepo sr = new SellerRepo();
            Seller seller = sr.GetSeller(id);
            return View(seller);
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
    }
}