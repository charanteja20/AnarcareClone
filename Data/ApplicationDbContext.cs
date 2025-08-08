using Microsoft.EntityFrameworkCore;
using AnarcareWeb.Models;

namespace AnarcareWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
    }
}
