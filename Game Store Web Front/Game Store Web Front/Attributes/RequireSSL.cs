using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Game_Store_Web_Front.Attributes
{
    class RequireSSL: RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            base.HandleNonHttpsRequest(filterContext);

            // redirect to HTTPS version of page
            string url = "https://" + filterContext.HttpContext.Request.Url.Host + ":" + 44300 + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url);
        }
    }
}
