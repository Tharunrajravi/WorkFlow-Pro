using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Employees")]
    public class Employee
    {
        public Employee()
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            Documents = new HashSet<Document>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee code is required.")]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [StringLength(20)]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Range(0, 10000000, ErrorMessage = "Salary must be a non-negative value.")]
        [Column(TypeName = "decimal")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string ProfileImagePath { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}

