using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Departments")]
    public class Department
    {
        public Department()
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            Employees = new HashSet<Employee>();
            Projects = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Department name must be between 2 and 100 characters.")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Department code is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Department code must be between 2 and 20 characters.")]
        public string Code { get; set; }

        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}

