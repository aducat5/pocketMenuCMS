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
            return View(id);
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