using PMCMS.BLL.Repos;
using PMCMS.BLL.Utility;
using PMCMS.DAL;
using PMCMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Filters;

namespace PMCMS.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "POST"); // origins, headers, methods
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private string GetClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return IPAddress.Parse(((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            }
            return string.Empty;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization != null)
                {
                    var authToken = actionContext.Request.Headers.Authorization.Parameter;

                    // decoding authToken we get decode value in 'Username:Password' format
                    string decodeauthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                    // spliting decodeauthToken using ':'
                    string[] arrUserNameandPassword = decodeauthToken.Split(':');

                    if (arrUserNameandPassword[0] != "HARDCODEDUNAMEFTWMFS" || arrUserNameandPassword[1] != "hardcodedpwdsftwmfs")
                    {
                        string logMessage = string.Format("A failed login attempt to api made from ip of '{0}', with credentials of '{1}'.", GetClientIpAddress(actionContext.Request), decodeauthToken);
                        Logger.LogAsync(logMessage, "ApiException");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    string logMessage = string.Format("A failed request attempt to api made from ip of '{0}', with no credentials.", GetClientIpAddress(actionContext.Request));
                    Logger.LogAsync(logMessage, "ApiException");
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                Logger.LogAsync(ex);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }

    public class SellerController : ApiController
    {
        [Route("api/seller")]
        [BasicAuthentication]
        [HttpPost]
        public IHttpActionResult GetSeller([FromBody]string id)
        {
            if (id == null) 
                return Json(new ApiResponse() { Status = false, Result = "Id cannot be null" });

            SellerPackage package = new SellerPackage();

            SellerRepo sr = new SellerRepo();
            Seller seller = sr.GetSeller(id.ToInt());

            if (seller == null)
                return Json(new ApiResponse() { Status = false, Result = "The restaurant you are seeking could not be found." });

            package.SellerName = seller.SellerName;
            package.SellerJSON = seller.SellerJSON;
            package.Data = new List<string>();

            if (seller.Menus.Count < 1)
                return Json(package);

            foreach (DAL.Menu menu in seller.Menus)
            {
                if (menu.IsDeleted)
                    continue;

                package.Data.Add(menu.ContentJSON);
            }

            return Json(package);
        }
    }
}