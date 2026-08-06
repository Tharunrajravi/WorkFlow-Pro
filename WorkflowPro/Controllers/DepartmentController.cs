using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WorkflowPro.Models;

namespace WorkflowPro.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Department
        public ActionResult Index(string search)
        {
            var departments = db.Departments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                departments = departments.Where(d => d.DepartmentName.Contains(search));
                ViewBag.Search = search;
            }

            return View(departments.OrderBy(d => d.DepartmentName).ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();

            return View(department);
        }

        // GET: Department/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new Department());
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "DepartmentName,Description")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.CreatedDate = DateTime.Now;
                db.Departments.Add(department);
                db.SaveChanges();
                TempData["Success"] = "Department created successfully.";
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Department/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();

            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName,Description,CreatedDate")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Department updated successfully.";
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Department/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();

            ViewBag.HasEmployees = db.Employees.Any(e => e.DepartmentId == id);

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();

            bool hasEmployees = db.Employees.Any(e => e.DepartmentId == id);
            if (hasEmployees)
            {
                TempData["Error"] = "This department has employees assigned to it and cannot be deleted.";
                return RedirectToAction("Delete", new { id });
            }

            db.Departments.Remove(department);
            db.SaveChanges();
            TempData["Success"] = "Department deleted successfully.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
