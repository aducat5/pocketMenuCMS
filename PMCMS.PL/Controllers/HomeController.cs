using PMCMS.BSL.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.PL.Controllers
{
    public class HomeController : Controller
    {
        [UserAuth]
        public ActionResult Dashboard() => View();
        public ActionResult Landing() => View(); 
        public ActionResult NotFound() => View();
    }
}