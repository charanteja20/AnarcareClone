using System;
using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Availability { get; set; } = string.Empty;

        // Made nullable to prevent FormatException on bad DB data
        public DateTime? SubmittedAt { get; set; } 
    }
}
