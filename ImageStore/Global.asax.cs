using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace ImageStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("IE8")
            {
                ContextCondition = (context => context.Request.UserAgent.Contains("MSIE 8"))
            });
        }
    }
}
