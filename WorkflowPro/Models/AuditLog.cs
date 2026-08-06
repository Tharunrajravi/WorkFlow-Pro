using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("AuditLogs")]
    public class AuditLog
    {
        [Key]
        public long AuditLogId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(100)]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string Action { get; set; }

        [StringLength(100)]
        public string EntityName { get; set; }

        [StringLength(50)]
        public string EntityId { get; set; }

        public string Details { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
