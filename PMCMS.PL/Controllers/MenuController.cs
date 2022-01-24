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
            if (id == 1)
            {
                string fileName = "etcetera.pdf";
                string filePath = Server.MapPath("~/Content/src/assets/docs/" + fileName);
                string mimeType = MimeMapping.GetMimeMapping(filePath);

                byte[] stream = System.IO.File.ReadAllBytes(filePath);
                return File(stream, mimeType);
            }

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
            User currentUser = Session["user"] as User;
            List<Seller> sellersOfUser = sr.GetSellersOfUser(currentUser.UserID);
            if (sellersOfUser.Count > 0)
                return View(sellersOfUser);
            else
                return RedirectToAction("New", "Restaurant");
        }

        [UserAuth]
        public JsonResult Save(string menuData)
        {
            ApiResponse response = new ApiResponse();
            User currentUser = Session["user"] as User;
            List<Seller> sellersOfUser = sr.GetSellersOfUser(currentUser.UserID);
            Seller firstSeller = sellersOfUser[0];

            firstSeller.SellerJSON = menuData;
            firstSeller.UpdateDate = DateTime.Now;

            firstSeller = sr.UpdateSeller(firstSeller, out string result);
            response.Status = firstSeller != null;
            response.Result = result;
            return Json(response);
        }
    }
}