using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowPro.Models;
using WorkflowPro.Models.ViewModels;

namespace WorkflowPro.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        private static readonly string[] AllowedPhotoExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const int MaxPhotoSizeBytes = 5 * 1024 * 1024; // 5 MB

        // GET: Employee
        public ActionResult Index(string search)
        {
            var employees = db.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                employees = employees.Where(e =>
                    e.FirstName.Contains(search) ||
                    e.LastName.Contains(search) ||
                    e.EmployeeCode.Contains(search) ||
                    e.Email.Contains(search));
                ViewBag.Search = search;
            }

            return View(employees.OrderBy(e => e.FirstName).ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null) return HttpNotFound();

            return View(employee);
        }

        // GET: Employee/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new EmployeeViewModel
            {
                Departments = new SelectList(db.Departments.OrderBy(d => d.DepartmentName), "DepartmentId", "DepartmentName")
            };
            return View(model);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(EmployeeViewModel model)
        {
            if (db.Employees.Any(e => e.EmployeeCode == model.EmployeeCode))
            {
                ModelState.AddModelError("EmployeeCode", "This employee code is already in use.");
            }

            string photoError = ValidatePhoto(model.ProfilePhoto, required: false);
            if (photoError != null) ModelState.AddModelError("ProfilePhoto", photoError);

            if (!ModelState.IsValid)
            {
                model.Departments = new SelectList(db.Departments.OrderBy(d => d.DepartmentName), "DepartmentId", "DepartmentName", model.DepartmentId);
                return View(model);
            }

            var employee = new Employee
            {
                EmployeeCode = model.EmployeeCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                DepartmentId = model.DepartmentId,
                Designation = model.Designation,
                Salary = model.Salary,
                CreatedDate = DateTime.Now
            };

            if (model.ProfilePhoto != null && model.ProfilePhoto.ContentLength > 0)
            {
                employee.ProfilePhotoPath = SavePhoto(model.ProfilePhoto);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            TempData["Success"] = "Employee created successfully.";
            return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = db.Employees.Find(id);
            if (employee == null) return HttpNotFound();

            var model = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                DepartmentId = employee.DepartmentId,
                Designation = employee.Designation,
                Salary = employee.Salary,
                ExistingProfilePhotoPath = employee.ProfilePhotoPath,
                Departments = new SelectList(db.Departments.OrderBy(d => d.DepartmentName), "DepartmentId", "DepartmentName", employee.DepartmentId)
            };

            return View(model);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EmployeeViewModel model)
        {
            if (db.Employees.Any(e => e.EmployeeCode == model.EmployeeCode && e.EmployeeId != model.EmployeeId))
            {
                ModelState.AddModelError("EmployeeCode", "This employee code is already in use.");
            }

            string photoError = ValidatePhoto(model.ProfilePhoto, required: false);
            if (photoError != null) ModelState.AddModelError("ProfilePhoto", photoError);

            if (!ModelState.IsValid)
            {
                model.Departments = new SelectList(db.Departments.OrderBy(d => d.DepartmentName), "DepartmentId", "DepartmentName", model.DepartmentId);
                return View(model);
            }

            var employee = db.Employees.Find(model.EmployeeId);
            if (employee == null) return HttpNotFound();

            employee.EmployeeCode = model.EmployeeCode;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.DepartmentId = model.DepartmentId;
            employee.Designation = model.Designation;
            employee.Salary = model.Salary;

            if (model.ProfilePhoto != null && model.ProfilePhoto.ContentLength > 0)
            {
                DeletePhotoFile(employee.ProfilePhotoPath);
                employee.ProfilePhotoPath = SavePhoto(model.ProfilePhoto);
            }

            db.SaveChanges();

            TempData["Success"] = "Employee updated successfully.";
            return RedirectToAction("Index");
        }

        // GET: Employee/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null) return HttpNotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null) return HttpNotFound();

            DeletePhotoFile(employee.ProfilePhotoPath);

            db.Employees.Remove(employee);
            db.SaveChanges();

            TempData["Success"] = "Employee deleted successfully.";
            return RedirectToAction("Index");
        }

        // ---- helpers ----

        private string ValidatePhoto(HttpPostedFileBase file, bool required)
        {
            if (file == null || file.ContentLength == 0)
            {
                return required ? "Profile photo is required." : null;
            }

            string ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || Array.IndexOf(AllowedPhotoExtensions, ext) < 0)
            {
                return "Only JPG, PNG or GIF images are allowed.";
            }

            if (file.ContentLength > MaxPhotoSizeBytes)
            {
                return "Photo must be 5 MB or smaller.";
            }

            return null;
        }

        private string SavePhoto(HttpPostedFileBase file)
        {
            string folder = Server.MapPath(ConfigurationManager.AppSettings["EmployeePhotosUploadPath"]);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string ext = Path.GetExtension(file.FileName);
            string storedName = $"{Guid.NewGuid():N}{ext}";
            file.SaveAs(Path.Combine(folder, storedName));

            return storedName;
        }

        private void DeletePhotoFile(string storedFileName)
        {
            if (string.IsNullOrWhiteSpace(storedFileName)) return;

            string folder = Server.MapPath(ConfigurationManager.AppSettings["EmployeePhotosUploadPath"]);
            string path = Path.Combine(folder, storedFileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
