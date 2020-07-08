using PMCMS.BLL.Repos;
using PMCMS.BLL.Utility;
using PMCMS.BSL.Authorization;
using PMCMS.DAL;
using System;
using System.Collections.Generic;
using System.IO;
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
        public JsonResult UploadFile()
        {
            ApiResponse response = new ApiResponse();
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + file.FileName;
                string extension = fileName.Split('.').Last();
                if (extension == "jpg" || extension == "jpeg" || extension == "png" && file.ContentType.Split('/')[0] == "image")
                {
                    string fullPath = Path.Combine(Server.MapPath("~/Content/src/assets/images/menu/"), fileName);
                    file.SaveAs(fullPath);

                    response.Result = true;
                    response.Result = new { message = "File uploaded on: http://pma.ist/content/src/assets/images/menu/" + fileName, url = "http://pma.ist/content/src/assets/images/menu/" + fileName};
                }
                else
                {
                    response.Status = false;
                    response.Result = "The filetype you are trying to upload is not allowed. Please try jpg, jpeg or png.";
                }
            }
            else
            {
                response.Status = false;
                response.Result = "There is no file to upload";
            }
            return Json(response);
        }
    }
}