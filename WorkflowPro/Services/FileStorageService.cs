using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using WorkflowPro.Models;

namespace WorkflowPro.Services
{
    public class FileStorageService
    {
        private readonly EmployeeDBContext _context;
        private readonly string _baseUploadPath;

        public FileStorageService(EmployeeDBContext context)
        {
            _context = context ?? new EmployeeDBContext();
            _baseUploadPath = HttpContext.Current != null 
                ? HttpContext.Current.Server.MapPath("~/Uploads")
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
        }

        public FileStorageService(EmployeeDBContext context, string baseUploadPath)
        {
            _context = context ?? new EmployeeDBContext();
            _baseUploadPath = baseUploadPath;
        }

        /// <summary>
        /// Saves file to local file system in designated category subfolder (Employees, Documents, Projects, Temp)
        /// and stores file metadata in SQL Server database.
        /// </summary>
        public async Task<FileMetadata> SaveFileAsync(HttpPostedFileBase file, string category, string uploadedBy)
        {
            if (uploadedBy == null) uploadedBy = "System";

            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentException("Uploaded file cannot be null or empty.");
            }

            string validCategory = NormalizeCategory(category);
            string targetFolder = Path.Combine(_baseUploadPath, validCategory);

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            string originalFileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(originalFileName);
            string storedFileName = string.Format("{0:N}_{1}{2}", Guid.NewGuid(), DateTime.UtcNow.Ticks, fileExtension);
            string physicalFilePath = Path.Combine(targetFolder, storedFileName);
            string relativePath = string.Format("~/Uploads/{0}/{1}", validCategory, storedFileName);

            // Save file payload to physical filesystem
            file.SaveAs(physicalFilePath);

            // Store metadata entity in SQL Server database
            var metadata = new FileMetadata
            {
                OriginalFileName = originalFileName,
                StoredFileName = storedFileName,
                RelativePath = relativePath,
                ContentType = file.ContentType,
                FileSizeByte = file.ContentLength,
                FolderCategory = validCategory,
                UploadedDate = DateTime.UtcNow,
                UploadedBy = uploadedBy
            };

            _context.FileMetadatas.Add(metadata);
            await _context.SaveChangesAsync();

            return metadata;
        }

        /// <summary>
        /// Retrieves physical file path from FileMetadata record.
        /// </summary>
        public string GetPhysicalPath(FileMetadata metadata)
        {
            if (metadata == null) return null;

            string targetFolder = Path.Combine(_baseUploadPath, metadata.FolderCategory);
            return Path.Combine(targetFolder, metadata.StoredFileName);
        }

        /// <summary>
        /// Deletes file from file system and removes metadata record from database.
        /// </summary>
        public async Task<bool> DeleteFileAsync(int metadataId)
        {
            var metadata = await _context.FileMetadatas.FindAsync(metadataId);
            if (metadata == null) return false;

            string physicalPath = GetPhysicalPath(metadata);
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }

            _context.FileMetadatas.Remove(metadata);
            await _context.SaveChangesAsync();
            return true;
        }

        private string NormalizeCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) return "Temp";

            switch (category.Trim().ToLowerInvariant())
            {
                case "employees":
                case "employee":
                    return "Employees";
                case "documents":
                case "document":
                    return "Documents";
                case "projects":
                case "project":
                    return "Projects";
                case "temp":
                default:
                    return "Temp";
            }
        }
    }
}

