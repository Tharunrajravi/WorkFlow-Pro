using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowPro.Models
{
    [Table("Settings")]
    public class Setting
    {
        public Setting()
        {
            UpdatedDate = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Setting key is required.")]
        [StringLength(100)]
        public string Key { get; set; }

        [Required(ErrorMessage = "Setting value is required.")]
        public string Value { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public DateTime UpdatedDate { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }
    }
}

