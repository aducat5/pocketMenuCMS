using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.PL.Controllers
{
    public class TriesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Menu menu)
        {
            return null;
            //try
            //{
            //    menu.IsDeleted = false;
            //    menu.CreateDate = DateTime.Now;
            //    menu.UpdateDate = DateTime.Now;

            //    var db = DBTool.GetInstance();

            //    db.Menus.Add(menu);
            //    db.SaveChanges();

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            //return View();
        }
    }
}