using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("FileMetadata")]
    public class FileMetadata
    {
        public FileMetadata()
        {
            UploadedDate = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string OriginalFileName { get; set; }

        [Required]
        [StringLength(255)]
        public string StoredFileName { get; set; }

        [Required]
        [StringLength(500)]
        public string RelativePath { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public long FileSizeByte { get; set; }

        [Required]
        [StringLength(50)]
        public string FolderCategory { get; set; } // "Employees", "Documents", "Projects", "Temp"

        public DateTime UploadedDate { get; set; }

        [StringLength(100)]
        public string UploadedBy { get; set; }
    }
}

