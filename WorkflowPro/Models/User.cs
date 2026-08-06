using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string HR = "HR";
        public const string Manager = "Manager";
        public const string Employee = "Employee";
    }

    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required, StringLength(150)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string PasswordHash { get; set; }

        [Required, StringLength(256)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public int FailedLoginCount { get; set; }

        public DateTime? LastLoginOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
