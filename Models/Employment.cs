using System;
using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class Employment
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        public string? Comments { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public string? ResumeUrl { get; set; }
    }
}
