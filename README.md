# 🧠 CRM Backend Microservice

A scalable, testable CRM system with JWT-based authentication, Redis caching, PostgreSQL storage, and clean DDD practices — all orchestrated with Docker. Built for real-world scenarios like customer tracking, lead generation, deal flow, and analytics.

---

## 🚀 Technology Stack

| Technology                | Purpose                                                                 |
|---------------------------|-------------------------------------------------------------------------|
| **PostgreSQL (Dockerized)** | Relational DB in a container for easy setup and scalability.        |
| **Redis**                 | In-memory caching for faster performance and reduced DB load.          |
| **Entity Framework Core** | ORM to manage PostgreSQL via strongly-typed C# entities.              |
| **AutoMapper**            | DTO ↔ Entity mapping for clean separation of concerns.                 |
| **ASP.NET Core Web API**  | Powers secure REST endpoints with JWT authentication.                  |
| **Docker Compose**        | Spins up the API, Redis, and PostgreSQL in one command.                |
| **Domain-Driven Design**  | Code is modeled around real business rules for maintainability.        |
| **Clean Architecture**    | Layered structure for testability, scalability, and separation of concerns.|

---

## 🔐 Authentication

- **Login** with:
  - Username: `admin`
  - Password: `Admin2025!`
- On success, a **JWT token** is returned.
- Include the token in every request using the custom header:
  ```
  X-Authorization: {your-token}
  ```

---

## 🧪 API Features

- ✅ Full **CRUD support** for Customers, Leads, Opportunities
- 🧠 Real business logic: Analytics, Status handling, Deal stage tracking
- 🔄 Mock testing mode with in-memory data for isolated testing
- 🕒 Redis caching for `Customer` data (30 min expiration)
- 🔧 [Switch between **In-Memory** & **PostgreSQL** via config](#-switch-between-in-memory-and-postgresql)
- 🧪 **xUnit** tests for key endpoints

---

## 📦 Postman Collection

You can test the entire API using the bundled collection:

 👉 Import  on **Postman** the **file named** : ***CRM CRUD Test Custom APIs*** & CRM_Batch_Seeding.postman_collection located at : **MyApiProject.API**

### Example: `GET` Address (Mock Mode)

```json
POST /api/address
Headers:
  X-Authorization: {your-jwt}
  X-RequestId: mock
Body:
{
  "internalId": "int1",
  "externalId": "ext1",
  "addressId": "1"
}
```

---

## ⚙️ Setup & Installation

### 1️⃣ Run Dependencies via Docker
navigate  cmd/PowerShell/Bash on  \MyApiProject\MyApiProject.API and run
```bash
docker-compose up -d
```

- PostgreSQL: `localhost:5432`
- Redis: `localhost:6379`

---

### 2️⃣ Configure JWT Settings

#### Option A: 🔐 Secret Manager (Dev)

In `Program.cs`, uncomment:
```csharp
// var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
```

Then run:

```powershell
dotnet user-secrets init --project "Path\To\YourProject.csproj"
dotnet user-secrets set "JwtSettings:Issuer" "test.gr"
dotnet user-secrets set "JwtSettings:Audience" "test"
dotnet user-secrets set "JwtSettings:Key" "justADummyTokenKeyForDummyTest2025!"
dotnet user-secrets set "JwtSettings:ExpiryMinutes" "60"
```

#### Option B: 🌍 Environment Variables

In `Program.cs`, uncomment the env variable block. Then in PowerShell:

```powershell
[System.Environment]::SetEnvironmentVariable("JWT_ISSUER", "test.gr", "User")
[System.Environment]::SetEnvironmentVariable("JWT_AUDIENCE", "test", "User")
[System.Environment]::SetEnvironmentVariable("JWT_KEY", "justADummyTokenKeyForDummyTest2025!", "User")
[System.Environment]::SetEnvironmentVariable("JWT_EXPIRY_MINUTES", "60", "User")
```

---

## 🛠 Switch Between In-Memory and PostgreSQL

Edit `appsettings.json`:

```json
"UseInMemoryDatabase": true // or false
```

- `true`: Enables mock data with Redis Cache.
- `false`: Uses actual PostgreSQL DB.

---

## 🚧 Under Construction

- ⏳ More advanced domain validation rules
- 🧪 More xUnit test coverage (services + API layers)
- 📈 Add Swagger docs for easy endpoint discovery

---
