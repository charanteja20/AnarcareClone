// public class AdminUser
// {
//     public int Id { get; set; }
//     public string Username { get; set; }
//     public string PasswordHash { get; set; } // Store hashed password
// }

namespace AnarcareWeb.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }
}
