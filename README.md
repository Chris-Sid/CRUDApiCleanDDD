### Features

- Login creates JWT (username: admin Password: Admin2025! )
- JWT Token is returned which is used in each request at the X-Authorization Header
- Test mock data (MockAddressData.cs)  for GET Address (RequestId: mock ,"internalId": "int1","externalId": "ext1" Authorization : JWT token) and it returns mock data accordingly in Post it changes the Data if a Mock is tampered with
-  xUnit testing in Post Address Service.
- Set UseInMemoryDatabase true to test Customers CRUD on Redis Cache (Mock Data) working at the moment without JWT saved in Memory for 30minuts . Set UseInMemoryDatabase False to test CRUD ON PostgreSQL after of running Docker on both scenarios

### Under Construction
updates on services with best practices . Also it Will be added Mapper to fulfill requests and make it more efficient & readable.

# Installation Guide
##### Redis Cache & PostgreSQL with docker 
navigate to folder of project on CMD or bash and run
```bash
docker compose up -d
```
Redis will now be running on localhost:6379, using the exact same behavior as your original container.

### Configure JWT Settings
### Option A: 🔐 Using Secret Manager**
Uncomment the following line in Program.cs:

```csharp
// var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
```
Then in PowerShell, run:
```
cd D:\Documents\MyApiProject\MyApiProject
dotnet user-secrets init --project "D:\Documents\MyApiProject\MyApiProject\MyApiProject.API.csproj"

dotnet user-secrets set "JwtSettings:Issuer" "test.gr"
dotnet user-secrets set "JwtSettings:Audience" "test"
dotnet user-secrets set "JwtSettings:Key" "justADummyTokenKeyForDummyTest2025!"
dotnet user-secrets set "JwtSettings:ExpiryMinutes" "60"
```
#### Option B: 🌍 Using Environment Variables
Uncomment the following code block in Program.cs:
```csharp
var jwtSettings = new JwtSettings
{
    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    Key = Environment.GetEnvironmentVariable("JWT_KEY"),
    ExpiryMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES"), out var minutes) ? minutes : 60
};

```
Navigate to project path and Set environment variables in PowerShell:
```
[System.Environment]::SetEnvironmentVariable("JWT_ISSUER", "test.gr", "User")
[System.Environment]::SetEnvironmentVariable("JWT_AUDIENCE", "test", "User")
[System.Environment]::SetEnvironmentVariable("JWT_KEY", "justADummyTokenKeyForDummyTest2025!", "User")
[System.Environment]::SetEnvironmentVariable("JWT_EXPIRY_MINUTES", "60", "User")

```
###  Mock Data & Testing
🧪 Test Login
You can test login with:

**Username**: admin

**Password**: Admin2025!

A JWT token will be returned and should be passed as a header:

#### GET Address example of mock data
you can use Collection included on project named as : Test Custom APIs and import at postman and changing x-Authorization with JWT you generated will work or else use your request like below : 
```json
{
  "internalId": "int1",
  "externalId": "ext1"
  "X-Authorization":"your JWT returned from login"
  "X-RequestId":"mock"
  "addressId":"1"
}

```
