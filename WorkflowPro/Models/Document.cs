using System.ComponentModel.DataAnnotations;

namespace WorkflowPro.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150)]
        public string Title { get; set; }

        // Original file name as uploaded by the user, shown in the UI.
        [Required]
        [StringLength(255)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        // Actual name on disk under ~/Uploads/Documents (GUID-based, to avoid collisions).
        [Required]
        [StringLength(255)]
        public string StoredFileName { get; set; }

        [StringLength(100)]
        [Display(Name = "File Type")]
        public string ContentType { get; set; }

        [Display(Name = "Size (KB)")]
        public long FileSizeKB { get; set; }

        [Display(Name = "Uploaded By")]
        [StringLength(50)]
        public string UploadedBy { get; set; }

        [Display(Name = "Uploaded On")]
        public System.DateTime UploadedDate { get; set; } = System.DateTime.Now;
    }
}
