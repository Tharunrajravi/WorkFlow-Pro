using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("ProjectAssignments")]
    public class ProjectAssignment
    {
        [Key]
        public int ProjectAssignmentId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [StringLength(100)]
        public string RoleOnProject { get; set; }

        public DateTime AssignedOn { get; set; }

        public DateTime? RemovedOn { get; set; }
    }
}
