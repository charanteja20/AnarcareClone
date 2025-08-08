# anacare PROJECT

git add README.md
git commit -m "Trigger new build"
git push
Git commmit
# Anarcare Web Application

## Overview

Anarcare is a web application for a home health and hospice agency. It provides information about services, allows users to submit contact, referral, employment, and volunteer forms, and includes an admin dashboard for managing submissions.

---

## Table of Contents

- [Project Structure](#project-structure)
- [Technology Stack](#technology-stack)
- [How to Build & Run](#how-to-build--run)
- [Configuration](#configuration)
- [Frontend Overview](#frontend-overview)
- [Backend Overview](#backend-overview)
- [Database](#database)
- [Admin Panel](#admin-panel)
- [Updating Backend/Frontend Links](#updating-backendfrontend-links)
- [Deployment (Docker)](#deployment-docker)
- [Styling](#styling)
- [Accessibility & Internationalization](#accessibility--internationalization)
- [Contributing](#contributing)
- [License](#license)

---

## Project Structure

```
├── Controllers/           # ASP.NET MVC controllers (Admin, Home)
├── Data/                  # Entity Framework DbContext
├── Migrations/            # EF Core migrations
├── Models/                # Data models (ContactMessage, Volunteer, Referral, Employment, etc.)
├── ViewModels/            # View models for admin dashboard, etc.
├── Views/                 # Razor views (.cshtml) for all pages
│   ├── Home/              # Main site pages
│   └── Admin/             # Admin dashboard pages
├── wwwroot/               # Static assets (css, js, images, videos)
│   ├── css/               # Main stylesheet (site.css)
│   ├── js/                # Main scripts (site.js, script.js)
│   ├── images/            # Images used in the site
│   └── videos/            # Video backgrounds
├── appsettings.json       # Main configuration file
├── Program.cs             # ASP.NET Core entrypoint
├── Anarcareweb.csproj     # Project file
├── Dockerfile             # Docker build instructions
├── README.md              # Project documentation
```

---

## Technology Stack

- **Backend:** ASP.NET Core MVC (.NET 9)
- **Frontend:** Razor Views, HTML, CSS ([site.css](wwwroot/css/site.css)), JavaScript ([site.js](wwwroot/js/site.js))
- **Database:** SQL Server (Entity Framework Core)
- **Deployment:** Docker
- **Other:** Bootstrap, Bootstrap Icons, Google Fonts, AOS (Animate On Scroll), Google Translate

---

## How to Build & Run

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Node.js](https://nodejs.org/) (optional, for asset management)
- Docker (optional, for containerized deployment)

### Local Development

1. **Restore dependencies:**
   ```sh
   dotnet restore
   ```

2. **Apply migrations & seed admin user:**
   - Connection string is in `appsettings.json`.
   - Migrations are auto-applied on startup (`Program.cs`).

3. **Run the application:**
   ```sh
   dotnet run
   ```

4. **Access the site:**
   - Main site: `http://localhost:5000`
   - Admin panel: `http://localhost:5000/Admin/Login`

### Docker Deployment

1. **Build Docker image:**
   ```sh
   docker build -t anarcareweb .
   ```

2. **Run Docker container:**
   ```sh
   docker run -p 5000:80 anarcareweb
   ```

---

## Configuration

- **Database Connection:**  
  Edit `DefaultConnection` in [`appsettings.json`](appsettings.json).

- **Admin Credentials:**  
  Set in environment variables or `appsettings.json` (`AdminCredentials:Username`, `AdminCredentials:Password`).

- **Azure Blob Storage (for resumes):**  
  Update `AzureBlobBaseUrl` and `AzureBlobSasToken` in [`appsettings.json`](appsettings.json).

---

## Frontend Overview

- **Main Pages:**  
  - Home: [`Views/Home/Index.cshtml`](Views/Home/Index.cshtml)
  - About Us: [`Views/Home/aboutus.cshtml`](Views/Home/aboutus.cshtml)
  - Services: [`Views/Home/services.cshtml`](Views/Home/services.cshtml)
  - Hospice Care: [`Views/Home/hospicecare.cshtml`](Views/Home/hospicecare.cshtml)
  - Home Health: [`Views/Home/homehealth.cshtml`](Views/Home/homehealth.cshtml)
  - Referrals: [`Views/Home/referrals.cshtml`](Views/Home/referrals.cshtml)
  - Careers/Employment/Volunteer: [`Views/Home/careers.cshtml`](Views/Home/careers.cshtml), [`Views/Home/employment.cshtml`](Views/Home/employment.cshtml), [`Views/Home/volunteer.cshtml`](Views/Home/volunteer.cshtml)

- **Forms:**  
  - Contact: [`Views/Home/Index.cshtml`](Views/Home/Index.cshtml)
  - Referral: [`Views/Home/referrals.cshtml`](Views/Home/referrals.cshtml)
  - Employment: [`Views/Home/employment.cshtml`](Views/Home/employment.cshtml)
  - Volunteer: [`Views/Home/volunteer.cshtml`](Views/Home/volunteer.cshtml)

- **Styling:**  
  - Main stylesheet: [`wwwroot/css/site.css`](wwwroot/css/site.css)
  - Responsive design via media queries.

- **JavaScript:**  
  - Main scripts: [`wwwroot/js/site.js`](wwwroot/js/site.js)
  - Handles navigation, form toggles, smooth scroll, language popup, accessibility view.

---

## Backend Overview

- **Controllers:**
  - [`Controllers/HomeController.cs`](Controllers/HomeController.cs): Handles all public pages and form submissions.
  - [`Controllers/AdminController.cs`](Controllers/AdminController.cs): Handles admin login, dashboard, and viewing submissions.

- **Models:**
  - [`Models/ContactMessage`](Models/)
  - [`Models/Volunteer`](Models/)
  - [`Models/Referral`](Models/)
  - [`Models/Employment`](Models/)

- **ViewModels:**
  - [`ViewModels/AdminDashboardViewModel`](ViewModels/AdminDashboardViewModel.cs): Used for admin dashboard stats.

- **Form Submission Logic:**
  - Contact, Referral, Employment, Volunteer forms are handled in [`HomeController`](Controllers/HomeController.cs).
  - Employment resumes are uploaded to Azure Blob Storage.

---

## Database

- **Entity Framework Core** is used for ORM.
- **Migrations** are stored in [`Migrations/`](Migrations/).
- **Tables:** ContactMessages, Volunteers, Referrals, Employments, AdminUsers.

---

## Admin Panel

- **Login:** `/Admin/Login`
- **Dashboard:** `/Admin/Dashboard` (shows total counts)
- **View Submissions:**  
  - Contact Messages: `/Admin/ContactMessages`
  - Volunteers: `/Admin/Volunteers`
  - Referrals: `/Admin/Referrals`
  - Employment: `/Admin/Employment`

---

## Updating Backend/Frontend Links

- **Backend API Endpoints:**  
  - Change controller actions in [`Controllers/HomeController.cs`](Controllers/HomeController.cs) and [`Controllers/AdminController.cs`](Controllers/AdminController.cs).
  - Update form action URLs in Razor views (e.g., `asp-action="SubmitContact"`).

- **Frontend Navigation Links:**  
  - Update navigation in [`Views/Shared/_Layout.cshtml`](Views/Shared/_Layout.cshtml).
  - Update page links in Razor views.

- **Static Assets:**  
  - Update images/videos in [`wwwroot/images/`](wwwroot/images/) and [`wwwroot/videos/`](wwwroot/videos/).
  - Update CSS in [`wwwroot/css/site.css`](wwwroot/css/site.css).
  - Update JS in [`wwwroot/js/site.js`](wwwroot/js/site.js).

---

## Deployment (Docker)

- **Dockerfile:**  
  - Located at [`Dockerfile`](Dockerfile).
  - Uses multi-stage build for optimized image.
  - Update exposed ports or entrypoint as needed.

---

## Styling

- **Main CSS:**  
  - [`wwwroot/css/site.css`](wwwroot/css/site.css) contains all styles, including responsive rules and page-specific sections.
  - Footer, header, cards, forms, and all sections are styled here.

---

## Accessibility & Internationalization

- **Accessibility:**  
  - Toggle accessibility view via JS (`toggleAccessibilityView()` in [`site.js`](wwwroot/js/site.js)).
  - Accessible color contrast and font sizes.

- **Internationalization:**  
  - Google Translate widget is integrated (see [`Views/Shared/_Layout.cshtml`](Views/Shared/_Layout.cshtml)).
  - Language popup for English/Spanish selection.

---

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Make changes (see [Updating Backend/Frontend Links](#updating-backendfrontend-links)).
4. Test locally.
5. Submit a pull request.

---

## License

- See [`wwwroot/lib/bootstrap/LICENSE`](wwwroot/lib/bootstrap/LICENSE) and [`wwwroot/lib/jquery-validation/LICENSE.md`](wwwroot/lib/jquery-validation/LICENSE.md) for third-party libraries.
- Project code is copyright Anarcare.

---

## Contact

- Email: administrator@anarcare.com
- Address: 13601 Woodforest Blvd, Houston, TX 77015
- Phone: (713) 330-1964

---

## Quick Start

```sh
dotnet restore
dotnet run
```

---

## Notes

- **To update backend links:** Change controller actions and routes in [`Controllers/HomeController.cs`](Controllers/HomeController.cs) and [`Controllers/AdminController.cs`](Controllers/AdminController.cs).
- **To update frontend links:** Change navigation and hrefs in [`Views/Shared/_Layout.cshtml`](Views/Shared/_Layout.cshtml) and page-specific Razor views.
- **To update Azure Blob Storage:** Change keys in [`appsettings.json`](appsettings.json).
- **To update styling:** Edit [`wwwroot/css/site.css`](wwwroot/css/site.css).
- **To update scripts:** Edit [`wwwroot/js/site.js`](wwwroot/js/site.js).

---

## For Further Development

- Add unit/integration tests.
- Add more languages to Google Translate.
- Enhance accessibility features.
- Add more admin features (e.g., export submissions).