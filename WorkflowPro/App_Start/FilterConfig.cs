using System.Web.Mvc;

namespace WorkflowPro
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Every action requires an authenticated user by default.
            // Use [AllowAnonymous] on the Login action to exempt it.
            filters.Add(new AuthorizeAttribute());
        }
    }
}
