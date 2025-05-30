Install info:

Open CMD (prefer from visual studio) navigate to project folder like : cd D:\Documents\MyApiProject\MyApiProject and then run docker compose up -d (it will run the file docker-compose.yml)

Add Enviroment Variables or Secrets Manager:

Using Secrets Manager

For using secrets Manager 
on program cs uncomment : 
//var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

On Powershell:

cd D:\Documents\MyApiProject\MyApiProject 
dotnet user-secrets init --project "D:\Documents\MyApiProject\MyApiProject\MyApiProject.API.csproj"
dotnet user-secrets set "JwtSettings:Issuer" "test.gr" 
dotnet user-secrets set "JwtSettings:Audience" "test"
dotnet user-secrets set "JwtSettings:Key" "justADummyTokenKeyForDummyTest2025!" 
dotnet user-secrets set "JwtSettings:ExpiryMinutes" "60"

Using Enviromental Variables

on program cs uncomment : 
var jwtSettings = new JwtSettings
{
    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    Key = Environment.GetEnvironmentVariable("JWT_KEY"),
    ExpiryMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES"), out var minutes) ? minutes : 60
};

on Powershell:


[System.Environment]::SetEnvironmentVariable("JWT_ISSUER", "test.gr", "User")
[System.Environment]::SetEnvironmentVariable("JWT_AUDIENCE", "test", "User")
[System.Environment]::SetEnvironmentVariable("JWT_KEY", "justADummyTokenKeyForDummyTest2025!", "User")
[System.Environment]::SetEnvironmentVariable("JWT_EXPIRY_MINUTES", "60", "User")


INFO How to TEST MOCK DATA
if the user logs in (username: test Password: Test2025! ) 
then the JWT Token is returned which is used in each request in the X-Authorization Header
Since the there are no real data 
I put mock data (MockAddressData.cs)  for GET Address (RequestId: mock ,"internalId": "int1",
"externalId": "ext1" Authorization : JWT token) and it returns mock data accordingly in Post it changes the Data if a Mock is tampered with
Finally I did a dummy xUnit testing in Post Address Service.





