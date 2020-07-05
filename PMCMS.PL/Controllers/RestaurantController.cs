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
    public class RestaurantController : Controller
    {
        private readonly SellerRepo sr = new SellerRepo();
        private readonly EmployeeRepo er = new EmployeeRepo();
        [UserAuth]
        public ActionResult All() => View();

        [UserAuth]
        public ActionResult Edit()
        {
            User current = Session["user"] as User;
            List<Seller> sellersOfUser = sr.GetSellersOfUser(current.UserID);
            if (sellersOfUser.Count > 0)
                return View(sellersOfUser);
            else
                return RedirectToAction("New");
        }
        [UserAuth, HttpPost]
        public JsonResult Edit(string sellerName, string sellerJSON)
        {
            ApiResponse response = new ApiResponse();
            User current = Session["user"] as User;
            List<Seller> sellersOfUser = sr.GetSellersOfUser(current.UserID);
            if (sellersOfUser.Count > 0)
            {
                try
                {
                    Seller firstSeller = sellersOfUser.First();
                    firstSeller.SellerName = sellerName;
                    firstSeller.SellerJSON = sellerJSON;
                    firstSeller.UpdateDate = DateTime.Now;

                    firstSeller = sr.UpdateSeller(firstSeller, out string result);
                    response.Status = firstSeller != null;
                    response.Result = result;

                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.Result = ex.Message;
                    response.Exception = ex;
                    Logger.LogAsync(ex);
                }
            }
            else
            {
                response.Status = false;
                response.Result = "This action is not allowed.";
            }

            return Json(response);
        }

        [UserAuth]
        public ActionResult New() => View();

        [UserAuth, HttpPost]
        public JsonResult New(string SellerName, string SellerJSON)
        {
            ApiResponse response = new ApiResponse();

            Seller newSeller = new Seller()
            {
                SellerName = SellerName,
                SellerJSON = SellerJSON
            }; 
            
            try
            {
                newSeller = sr.InsertSeller(newSeller, out string result);
                if (newSeller != null)
                {
                    User currentUser = Session["user"] as User;
                    //set employee here
                    Employee firstCeo = new Employee()
                    {
                        SellerID = newSeller.SellerID,
                        UserID = currentUser.UserID
                    };

                    firstCeo = er.InsertEmployee(firstCeo);

                    response.Status = firstCeo != null;
                    response.Result = result;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Result = ex.Message;
                response.Exception = ex;
                Logger.LogAsync(ex);
            }

            return Json(response);
        }

        [UserAuth]
        public ActionResult Display() => View();

        [UserAuth]
        public ActionResult GetQr()
        {
            User currentUser = Session["user"] as User;
            List<Seller> sellersOfUser = sr.GetSellersOfUser(currentUser.UserID);
            if (sellersOfUser.Count > 0)
                return View(sellersOfUser);
            else
                return RedirectToAction("New");
        }
    }
}