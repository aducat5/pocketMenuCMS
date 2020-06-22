using PMCMS.BLL.Models;
using PMCMS.BLL.Utility;
using System;
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
                    LogType = logTypeID,
                    CreateDate = DateTime.Now,
                    LogMessage = exception.Message,
                    LogDetail = exception.InnerException + " - " + exception.StackTrace,
                    Machine = Server.MachineName
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

                if (logNeeded) Logger.Log(errorLog); ;
            }
            else
            {
                EventLog errorLog = new EventLog()
                {
                    LogType = logTypeID,
                    CreateDate = DateTime.Now,
                    LogMessage = "Bilinmeyen Hata",
                    Machine = Server.MachineName,
                    LogDetail = "Bilinmeyen Hata"
                };
                Response.Redirect("/Home/NotFound/");
            }
        }
    }
}
