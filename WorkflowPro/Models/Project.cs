using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Projects")]
    public class Project
    {
        public Project()
        {
            CreatedDate = DateTime.UtcNow;
            Documents = new HashSet<Document>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(150)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Project code is required.")]
        [StringLength(30)]
        public string ProjectCode { get; set; }

        [StringLength(100)]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Range(0, 1000000000, ErrorMessage = "Budget must be a positive number.")]
        [Column(TypeName = "decimal")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Project status is required.")]
        [StringLength(30)]
        public string Status { get; set; } // "Planning", "In Progress", "Completed", "On Hold"

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}

