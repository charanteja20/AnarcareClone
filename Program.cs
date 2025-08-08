using AnarcareWeb.Data;
using AnarcareWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// ✅ SHA-256 hashing for admin password
string ComputeSha256Hash(string rawData)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}

// ✅ Load admin credentials from environment or fallback to appsettings.json
string adminUsername = Environment.GetEnvironmentVariable("AdminUsername")
                        ?? config["AdminCredentials:Username"]
                        ?? "admin";

string rawPassword = Environment.GetEnvironmentVariable("AdminPassword")
                     ?? config["AdminCredentials:Password"]
                     ?? "admin";

string adminPassword = ComputeSha256Hash(rawPassword);

// ✅ Register services
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        config.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    ));

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show full errors in dev
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Safe DB migration and admin seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Apply migrations if needed
        db.Database.Migrate();

        // Ensure admin user exists
        var existingAdmin = db.AdminUsers.FirstOrDefault(a => a.Username == adminUsername);
        if (existingAdmin == null)
        {
            db.AdminUsers.Add(new AdminUser
            {
                Username = adminUsername,
                PasswordHash = adminPassword
            });
            db.SaveChanges();
        }
        else if (existingAdmin.PasswordHash != adminPassword)
        {
            existingAdmin.PasswordHash = adminPassword;
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Log error to console (visible in Azure or dev logs)
        Console.WriteLine("❌ Database migration or seeding failed: " + ex.Message);
    }
}

app.Run();
