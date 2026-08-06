using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkflowPro.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public System.DateTime CreatedDate { get; set; } = System.DateTime.Now;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
