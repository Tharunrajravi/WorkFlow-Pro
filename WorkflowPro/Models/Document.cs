using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Documents")]
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(260)]
        public string FileName { get; set; }

        [Required, StringLength(500)]
        public string FilePath { get; set; }

        [StringLength(20)]
        public string FileType { get; set; }

        public int? FileSizeKB { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("UploadedByUser")]
        public int UploadedByUserId { get; set; }
        public virtual User UploadedByUser { get; set; }

        public DateTime UploadedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
