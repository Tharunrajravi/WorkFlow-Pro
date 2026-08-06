using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowPro.Models;

namespace WorkflowPro.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        private const int MaxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
        private static readonly string[] BlockedExtensions = { ".exe", ".bat", ".cmd", ".msi", ".dll", ".sh", ".ps1" };

        // GET: Document
        public ActionResult Index(string search)
        {
            var documents = db.Documents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                documents = documents.Where(d => d.Title.Contains(search) || d.FileName.Contains(search));
                ViewBag.Search = search;
            }

            return View(documents.OrderByDescending(d => d.UploadedDate).ToList());
        }

        // GET: Document/Upload
        public ActionResult Upload()
        {
            return View(new Document());
        }

        // POST: Document/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(string title, HttpPostedFileBase file)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Title is required.");
            }

            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("File", "Please choose a file to upload.");
            }
            else
            {
                string ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                if (!string.IsNullOrEmpty(ext) && Array.IndexOf(BlockedExtensions, ext) >= 0)
                {
                    ModelState.AddModelError("File", "This file type is not allowed.");
                }
                else if (file.ContentLength > MaxFileSizeBytes)
                {
                    ModelState.AddModelError("File", "File must be 10 MB or smaller.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.SubmittedTitle = title;
                return View(new Document());
            }

            string folder = Server.MapPath(ConfigurationManager.AppSettings["DocumentsUploadPath"]);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string storedFileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
            file.SaveAs(Path.Combine(folder, storedFileName));

            var document = new Document
            {
                Title = title,
                FileName = Path.GetFileName(file.FileName),
                StoredFileName = storedFileName,
                ContentType = file.ContentType,
                FileSizeKB = file.ContentLength / 1024,
                UploadedBy = Session["FullName"] as string ?? User.Identity.Name,
                UploadedDate = DateTime.Now
            };

            db.Documents.Add(document);
            db.SaveChanges();

            TempData["Success"] = "Document uploaded successfully.";
            return RedirectToAction("Index");
        }

        // GET: Document/Download/5
        public ActionResult Download(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var document = db.Documents.Find(id);
            if (document == null) return HttpNotFound();

            string folder = Server.MapPath(ConfigurationManager.AppSettings["DocumentsUploadPath"]);
            string path = Path.Combine(folder, document.StoredFileName);

            if (!System.IO.File.Exists(path))
            {
                TempData["Error"] = "The file for this document could not be found on the server.";
                return RedirectToAction("Index");
            }

            string contentType = string.IsNullOrEmpty(document.ContentType)
                ? "application/octet-stream"
                : document.ContentType;

            return File(path, contentType, document.FileName);
        }

        // GET: Document/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var document = db.Documents.Find(id);
            if (document == null) return HttpNotFound();

            return View(document);
        }

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var document = db.Documents.Find(id);
            if (document == null) return HttpNotFound();

            string folder = Server.MapPath(ConfigurationManager.AppSettings["DocumentsUploadPath"]);
            string path = Path.Combine(folder, document.StoredFileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            db.Documents.Remove(document);
            db.SaveChanges();

            TempData["Success"] = "Document deleted successfully.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
