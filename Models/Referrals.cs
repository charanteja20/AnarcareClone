using System;
using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class Referral
    {
        public int Id { get; set; }

        [Required]
        public string ReferrerName { get; set; } = string.Empty;

        [Required]
        public string PatientName { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public string ServicesNeeded { get; set; } = string.Empty;

        public string? Comments { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
