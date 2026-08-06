using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(20)]
        public string EmployeeCode { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(150)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [StringLength(100)]
        public string Designation { get; set; }

        public DateTime DateOfJoining { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [ForeignKey("ReportingManager")]
        public int? ReportingManagerId { get; set; }
        public virtual Employee ReportingManager { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Employee> DirectReports { get; set; }
        public virtual ICollection<ProjectAssignment> ProjectAssignments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
