using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(200)]
        public string PasswordHash { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        // Simple role model: "Admin" or "User". Kept as a plain string
        // column rather than a separate Roles table to stay simple.
        [Required, StringLength(20)]
        public string Role { get; set; }

        public bool IsActive { get; set; } = true;

        public System.DateTime CreatedDate { get; set; } = System.DateTime.Now;
    }
}
