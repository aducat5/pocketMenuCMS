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
        public ActionResult Edit() => View();
        
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
        public ActionResult GetQr() => View();
    }
}