using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Documents")]
    public class Document
    {
        public Document()
        {
            UploadedDate = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Document title is required.")]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(50)]
        public string DocumentType { get; set; } // "Contract", "Report", "Invoice", "Policy", "General"

        [Required(ErrorMessage = "File path is required.")]
        [StringLength(500)]
        public string FilePath { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public long FileSizeByte { get; set; }

        public int? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [StringLength(100)]
        public string UploadedBy { get; set; }

        public DateTime UploadedDate { get; set; }
    }
}

