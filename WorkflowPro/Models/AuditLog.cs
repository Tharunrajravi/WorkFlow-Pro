using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("AuditLogs")]
    public class AuditLog
    {
        public AuditLog()
        {
            Timestamp = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Action name is required.")]
        [StringLength(100)]
        public string Action { get; set; }

        [Required(ErrorMessage = "Entity name is required.")]
        [StringLength(100)]
        public string EntityName { get; set; }

        [StringLength(50)]
        public string EntityId { get; set; }

        [StringLength(1000)]
        public string Details { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

