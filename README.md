
# 🧩 REST API – ASP.NET Core + Entity Framework Core

A complete REST API built using **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**.  
This API includes **User Login**, **Token Authentication**, and **CRUD for Students**.

---

## 🚀 Features

- User Login (Token stored in Users table)
- Token validation on every request
- Secure Student CRUD
- Get Student by Roll Number
- Clean folder structure (Controllers, Models, DTOs, Services)
- EF Core Migrations support
- SQL Server database

---

## 📂 Project Structure

YourProjectFolder
│── Controllers/
│── Models/
│── Data/
│── Migrations/
│── Services/
│── wwwroot/
│── appsettings.json
│── Program.cs
│── Startup.cs   (if .NET 5 or lower)
│── README.md    ← ALL DETAILS HERE
│── .gitignore
│── YourProject.csproj


## ⚙️ Database Setup

1️⃣ Update your SQL connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=RestApiDB;Trusted_Connection=True;"
}
```
# Run migrations:
`dotnet ef database update`

#▶️ How to Run the Project
`dotnet run`


#👉 Press F5 or click Start Debugging

#🔑 API Endpoints
#1️⃣ Get All Students (Token Required)
`GET: /api/students?token=YOUR_TOKEN`

#2️⃣ Get Student by Roll Number
`GET: /api/students/roll/{rollNumber}?token=YOUR_TOKEN`

#3️⃣ Add Student
`POST: /api/students
Body (JSON):`

`
{
  "fullName": "Sohail Khan",
  "fatherName": "Mr Khan",
  "phone": "1234567890",
  "email": "example@gmail.com",
  "course": "MCA",
  "city": "Mumbai",
  "address": "Andheri",
  "description": "Good student"
}
`

#🔐 Token Authentication (Custom)

`Your Users table contains a Token column.
Before any request API checks the token.`

Example:

`GET /api/students?token=123456`


#If token is invalid → returns 401 Unauthorized
# RestApi
