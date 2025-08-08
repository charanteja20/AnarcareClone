using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AnarcareWeb.Data;
using AnarcareWeb.Models;
using AnarcareWeb.ViewModels;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient; // ✅ Required for Azure SQL
using Microsoft.EntityFrameworkCore;
using System.Data.Common;


namespace AnarcareWeb.Controllers
{


    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
         public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        // Login Page
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string hashedPassword = ComputeSha256Hash(model.Password);
            var admin = _context.AdminUsers
                .FirstOrDefault(a => a.Username == model.Username && a.PasswordHash == hashedPassword);

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminUser", admin.Username ?? "");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminUser");
            return RedirectToAction("Login");
        }

        // Dashboard Summary
        public IActionResult Dashboard()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var viewModel = new AdminDashboardViewModel
            {
                TotalContact = _context.ContactMessages.Count(),
                TotalVolunteer = _context.Volunteers.Count(),
                TotalReferral = _context.Referrals.Count(),
                TotalEmployment = _context.Employments.Count()
            };

            return View(viewModel);
        }

        // Contact Messages
        public IActionResult ContactMessages()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var messages = _context.ContactMessages.ToList();
            return View(messages);
        }

        // Volunteer Submissions
        public IActionResult Volunteers()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var volunteers = new List<SafeVolunteerViewModel>();
            var dbConn = _context.Database.GetDbConnection();

            using (var conn = new Microsoft.Data.SqlClient.SqlConnection(dbConn.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name, Email, Phone, Availability, SubmittedAt FROM Volunteers";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        volunteers.Add(new SafeVolunteerViewModel
                        {
                            Name = reader.GetString(0),
                            Email = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Availability = reader.GetString(3),
                            SubmittedAtRaw = reader.IsDBNull(4) ? "N/A" : reader.GetValue(4).ToString()
                        });
                    }
                }
            }

            return View("Volunteers", volunteers);
        }

        // Referral Submissions
        public IActionResult Referrals()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var referrals = new List<Referral>();
            var dbConn = _context.Database.GetDbConnection();

            using (var conn = new Microsoft.Data.SqlClient.SqlConnection(dbConn.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT PatientName, Phone, ReferrerName, Comments, ServicesNeeded, SubmittedAt FROM Referrals";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referrals.Add(new Referral
                        {
                            PatientName = reader.GetString(0),
                            Phone = reader.GetString(1),
                            ReferrerName = reader.GetString(2),
                            Comments = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            ServicesNeeded = reader.GetString(4),
                            SubmittedAt = TryParseDateTime(reader.GetValue(5))
                        });
                    }
                }
            }

            return View(referrals);
        }

        // Employment Submissions
        public IActionResult Employment()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var employments = new List<Employment>();
            var dbConn = _context.Database.GetDbConnection();

            using (var conn = new Microsoft.Data.SqlClient.SqlConnection(dbConn.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT FullName, Email, Phone, Position, Comments, ResumeUrl, SubmittedAt FROM Employments";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employments.Add(new Employment
                        {
                            FullName = reader.GetString(0),
                            Email = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Position = reader.GetString(3),
                            Comments = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            ResumeUrl = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            SubmittedAt = TryParseDateTime(reader.GetValue(6))
                        });
                    }
                }
            }

            return View(employments);
        }

        // Helpers
        private bool IsAdminLoggedIn() =>
            !string.IsNullOrEmpty(HttpContext.Session.GetString("AdminUser"));

        private DateTime TryParseDateTime(object value)
        {
            if (value == null || value == DBNull.Value)
                return DateTime.MinValue;

            if (DateTime.TryParse(value.ToString(), out DateTime dt))
                return dt;

            return DateTime.MinValue;
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
