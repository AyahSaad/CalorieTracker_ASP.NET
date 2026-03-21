# 🥗 Calorie Tracker API

A full-featured RESTful API built with ASP.NET Core for tracking daily calories, meals, and nutrition.

---

## 🚀 Features

- **Authentication & Security** — JWT Authentication with Refresh Tokens, email confirmation, password reset
- **Role-Based Authorization** — User, Admin, SuperAdmin roles
- **Food Management** — Manual food entry + Edamam API integration with local caching
- **Meal Management** — Track Breakfast, Lunch, Dinner, Snack with automatic calorie calculation
- **Daily & Weekly Reports** — View consumed calories, remaining goal, and nutrition breakdown
- **Weight Tracking** — Log weekly weight with progress reports
- **Favorite Foods** — Save and reuse favorite foods directly in meals
- **User Profile** — Manage personal info and daily calorie goal

---

## 🛠 Technologies

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core Web API |
| Language | C# |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Authentication | ASP.NET Identity + JWT |
| Mapping | Mapster |
| External API | Edamam Food Database API |
| Architecture | 3-Tier (DAL, BLL, PL) + Repository Pattern |

---

## 📂 Project Structure

```
CalorieTracker/
├── CalorieTracker.DAL        # Data Access Layer
│   ├── Models                # Entity models
│   ├── Data                  # DbContext
│   ├── DTO                   # Request & Response DTOs
│   ├── Repository            # Repository interfaces & implementations
│   └── Utils                 # Seed data
├── CalorieTracker.BLL        # Business Logic Layer
│   ├── Interfaces            # Service interfaces
│   ├── Service               # Service implementations
│   └── MapsterConfigurations # Object mapping config
└── CalorieTracker.PL         # Presentation Layer
    └── Areas
        ├── Identity          # Auth endpoints
        ├── Admin             # Admin endpoints
        └── User              # User endpoints
```

---

## ⚙️ Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Edamam API credentials ([Get them here](https://developer.edamam.com))

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/AyahSaad/CalorieTracker_ASP.NET.git
cd CalorieTracker_ASP.NET
```

**2. Configure User Secrets**
```bash
cd CalorieTracker/CalorieTracker.PL
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your_connection_string"
dotnet user-secrets set "Jwt:SecretKey" "your_secret_key"
dotnet user-secrets set "EmailSettings:Email" "your_email"
dotnet user-secrets set "EmailSettings:Password" "your_email_password"
dotnet user-secrets set "Edamam:AppId" "your_app_id"
dotnet user-secrets set "Edamam:AppKey" "your_app_key"
```

**3. Run Migrations**
```bash
dotnet ef database update
```

**4. Run the project**
```bash
dotnet run
```

---

## 🔐 Default Users (Seed Data)

| Role | Email | Password |
|------|-------|----------|
| SuperAdmin | superAdmin@gmail.com | Pass@1122 |
| Admin | admin@gmail.com | Pass@1122 |
| User | user@gmail.com | Pass@1122 |

---

```

---

## 👩‍💻 Developer

**Ayah Saad** — Student @ Knowledge Academy ASP.NET course.
