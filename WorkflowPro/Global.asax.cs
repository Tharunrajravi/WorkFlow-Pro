using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WorkflowPro
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            System.Diagnostics.Trace.TraceError(ex?.ToString());
        }
    }
}

