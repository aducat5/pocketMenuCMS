using PMCMS.BLL.Utility;
using PMCMS.PL.Models;
using System;
using System.Net;
using System.Net.Http;
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
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers.Authorization.Parameter;

                // decoding authToken we get decode value in 'Username:Password' format
                string decodeauthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                // spliting decodeauthToken using ':'
                string[] arrUserNameandPassword = decodeauthToken.Split(':');

                if (arrUserNameandPassword[0] != "HARDCODEDUNAMEFTWMFS" || arrUserNameandPassword[1] == "hardcodedpwdsftwmfs")
                {
                    Logger.LogAsync("An attempt made with wrong credentials");
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                //Log the attempt here
                //login with credential
                //do nothing if succceeded
                //return throw exception if failed.
            }
            else
            {
                Logger.LogAsync("An attempt made with no auth header!");
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }

    public class SellerController : ApiController
    {
        [Route("api/seller")]
        [BasicAuthentication]
        [HttpPost]
        public IHttpActionResult GetSeller()
        {
            SellerPackage sellerPackage = new SellerPackage();
            return Json(sellerPackage);

            return Content(HttpStatusCode.BadRequest, "who are you so conversative yet so brave?");
        }
    }
}