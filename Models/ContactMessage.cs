using System;
using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
