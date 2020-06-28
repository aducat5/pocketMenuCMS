using PMCMS.BLL.Utility;
using PMCMS.DAL;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PMCMS.PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(Api.WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            int logTypeID = LogTypes.Error.ToInt();
            bool logNeeded = true;
            if (exception != null)
            {
                EventLog errorLog = new EventLog()
                {
                    LogType = logTypeID.ToString(),
                    CreateDate = DateTime.Now,
                    Message = exception.Message,
                    Detail = exception.InnerException + " - " + exception.StackTrace,
                    Source = "Global.asax"
                };

                switch (exception.GetType().Name)
                {
                    case "PageNotFoundException":
                        logNeeded = false;
                        Response.Redirect("/Home/NotFound/");
                        break;
                    case "LoginRequiredException":
                        logNeeded = false;
                        Response.Redirect("/Sign/In/");
                        break;
                    case "LogoutRequiredException":
                        logNeeded = false;
                        Response.Redirect("/Home/Index/");
                        break;
                    default:
                        Response.Redirect("/Home/NotFound/");
                        break;
                }

                if (logNeeded) 
                    Logger.LogAsync(errorLog);
            }
            else
            {
                EventLog errorLog = new EventLog()
                {
                    LogType = "Error",
                    CreateDate = DateTime.Now,
                    Message = "Bilinmeyen Hata",
                    Source = "Global.asax",
                    Detail = "Bilinmeyen Hata"
                };

                Logger.LogAsync(errorLog);

                Response.Redirect("/Home/NotFound/");
            }
        }
    }
}
