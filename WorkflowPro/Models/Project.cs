using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    public static class ProjectStatus
    {
        public const string Planned = "Planned";
        public const string InProgress = "InProgress";
        public const string OnHold = "OnHold";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
    }

    public static class ProjectPriority
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
        public const string Critical = "Critical";
    }

    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required, StringLength(20)]
        public string ProjectCode { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("ProjectManager")]
        public int? ProjectManagerId { get; set; }
        public virtual Employee ProjectManager { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required, StringLength(30)]
        public string Status { get; set; }

        [Required, StringLength(20)]
        public string Priority { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<ProjectAssignment> ProjectAssignments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
