using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WorkflowPro.Infrastructure;

namespace WorkflowPro
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            try
            {
                // Creates a default admin/Admin@123 account the first time the
                // app runs against an empty Users table. No-op afterwards.
                DbSeeder.SeedAdminUser();
            }
            catch (Exception ex)
            {
                // Database may not be reachable/created yet on first deploy -
                // don't prevent the app from starting because of it.
                System.Diagnostics.Trace.TraceWarning("Admin seed skipped: " + ex.Message);
            }
        }

        // Runs on every request once the Forms Authentication cookie has been
        // validated. We decrypt the ticket, pull the role out of UserData and
        // attach a role-aware IPrincipal so [Authorize(Roles = "...")] works.
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return;
            }

            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket == null || ticket.Expired)
                {
                    return;
                }

                string role = ticket.UserData ?? string.Empty;
                var identity = new System.Security.Principal.GenericIdentity(ticket.Name);
                var roles = string.IsNullOrWhiteSpace(role) ? new string[0] : new[] { role };
                var principal = new CustomPrincipal(identity, roles);

                HttpContext.Current.User = principal;
                System.Threading.Thread.CurrentPrincipal = principal;
            }
            catch (Exception)
            {
                // Malformed/tampered cookie - treat as anonymous.
                FormsAuthentication.SignOut();
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                // Kept intentionally simple - no external logging framework.
                // Errors surface via the Error view / IIS failed request tracing.
                System.Diagnostics.Trace.TraceError(exception.ToString());
            }
        }
    }
}
