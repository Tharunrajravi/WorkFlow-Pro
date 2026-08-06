using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            AuditLogs = new HashSet<AuditLog>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password hash is required.")]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User role is required.")]
        [StringLength(30)]
        public string Role { get; set; } // "Admin", "Manager", "Employee", "HR", "Finance"

        public bool IsActive { get; set; }

        public int? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}

