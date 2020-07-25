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

        [UserAuth]//TODO: put time barier here
        public JsonResult UploadFile()
        {
            ApiResponse response = new ApiResponse();
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files[0];
                    
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + file.FileName;
                    fileName = fileName.ClearText();
                    fileName = fileName.Replace(' ', '-');
                    fileName = fileName.RemoveAccent();

                    string extension = fileName.Split('.').Last();
                    if (extension == "jpg" || extension == "jpeg" || extension == "png" && file.ContentType.Split('/')[0] == "image")
                    {
                        string saveFolder = "~/Content/src/assets/images/menu/";
                        //_ = Directory.CreateDirectory(Server.MapPath(saveFolder));
                        string fullPath = Path.Combine(Server.MapPath(saveFolder), fileName);
                        file.SaveAs(fullPath);

                        response.Status = true;
                        response.Result = new { message = "File uploaded on: https://pma.ist/content/src/assets/images/menu/" + fileName, url = "https://pma.ist/content/src/assets/images/menu/" + fileName };
                    }
                    else
                    {
                        response.Status = false;
                        response.Result = "The filetype you are trying to upload is not allowed. Please try jpg, jpeg or png.";
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogAsync(ex);
                    response.Status = false;
                    response.Exception = ex;
                    response.Result = ex.Message;
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