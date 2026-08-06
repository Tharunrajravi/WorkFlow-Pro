using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WorkflowPro.Infrastructure;
using WorkflowPro.Models;
using WorkflowPro.Models.ViewModels;

namespace WorkflowPro.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            var user = db.Users.FirstOrDefault(u =>
                u.Username == model.Username && u.IsActive);

            if (user == null || !PasswordHasher.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            // Role travels inside the encrypted ticket's UserData - read back
            // in Global.asax.Application_PostAuthenticateRequest.
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: user.Username,
                issueDate: System.DateTime.Now,
                expiration: System.DateTime.Now.AddMinutes(model.RememberMe ? 60 * 24 * 7 : 60),
                isPersistent: model.RememberMe,
                userData: user.Role);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = Request.IsSecureConnection,
                Expires = ticket.IsPersistent ? ticket.Expiration : System.DateTime.MinValue
            };
            Response.Cookies.Add(authCookie);

            // Also stash the display name for the dashboard welcome message.
            Session["FullName"] = user.FullName;

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
