using System.Linq;
using System.Web.Mvc;
using WorkflowPro.Models;
using WorkflowPro.Models.ViewModels;

namespace WorkflowPro.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalEmployees = db.Employees.Count(),
                TotalDepartments = db.Departments.Count(),
                TotalDocuments = db.Documents.Count(),
                WelcomeName = Session["FullName"] as string ?? User.Identity.Name
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
