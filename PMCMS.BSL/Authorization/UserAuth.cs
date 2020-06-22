using PMCMS.BLL.CustomExceptions;
using System.Web;
using System.Web.Mvc;

namespace PMCMS.BSL.Authorization
{
    public class UserAuth : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["user"] != null)
                return true;
            else
            {
                throw new LoginRequiredException();
            }
        }
    }
}
