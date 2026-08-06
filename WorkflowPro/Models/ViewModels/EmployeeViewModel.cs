using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace WorkflowPro.Models.ViewModels
{
    public class EmployeeViewModel
    {
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
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 99999999.99, ErrorMessage = "Enter a valid salary amount.")]
        public decimal Salary { get; set; }

        [Display(Name = "Profile Photo")]
        public HttpPostedFileBase ProfilePhoto { get; set; }

        // Existing photo path, used to render current photo on the Edit view.
        public string ExistingProfilePhotoPath { get; set; }

        public SelectList Departments { get; set; }
    }
}
