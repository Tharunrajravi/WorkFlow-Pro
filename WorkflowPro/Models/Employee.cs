using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee code is required.")]
        [StringLength(20)]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [StringLength(100)]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 99999999.99, ErrorMessage = "Enter a valid salary amount.")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        // Relative path under ~/Uploads/Employees, e.g. "3fae21.jpg". Null/empty = no photo.
        [StringLength(260)]
        [Display(Name = "Profile Photo")]
        public string ProfilePhotoPath { get; set; }

        [Display(Name = "Joined On")]
        [DataType(DataType.Date)]
        public System.DateTime CreatedDate { get; set; } = System.DateTime.Now;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
