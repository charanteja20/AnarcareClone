namespace AnarcareWeb.ViewModels
{
    public class SafeVolunteerViewModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Availability { get; set; }
        public string? SubmittedAtRaw { get; set; } // treat all as strings
    }
}
