using PMCMS.BLL.Repos;
using PMCMS.BLL.Utility;
using PMCMS.BSL.Authorization;
using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease;

namespace PMCMS.PL.Controllers
{
    public class MenuController : Controller
    {
        private readonly SellerRepo sr = new SellerRepo();

        public ActionResult Display(int id)
        {
            string machine = HttpContext.Request.UserAgent;
            string ip = HttpContext.Request.UserHostAddress;
            string logMessage = string.Format("{0}|{1}", machine, ip);

            SellerRepo sr = new SellerRepo();
            Seller seller = sr.GetSeller(id); 
            
            Logger.Log(logMessage);
            return View(seller);
        }

        [UserAuth]
        public ActionResult Build()
        {
            return View();
        }
    }
}