if the user logs in (username: OneDealer Password: OneDealer2025! ) 
then the JWT Token is returned which is used in each request in the X-Authorization Header
Since the there are no real data 
I put mock data (MockAddressData.cs)  for GET Address (RequestId: mock ,"internalId": "int1",
"externalId": "ext1" Authorization : JWT token) and it returns mock data accordingly in Post it changes the Data if a Mock is tampered with
Finally I did a dummy xUnit testing in Post Address Service.

$env:JWT_ISSUER = "OneDealer.gr"
$env:JWT_AUDIENCE = "test"
$env:JWT_KEY = "onedealerTokenKeyForDummyTest2025!"
$env:JWT_EXPIRY_MINUTES = "60"

cd "D:\Documents\MyApiProject\MyApiProject\MyApiProject.API.csproj"
dotnet user-secrets init --project 
dotnet user-secrets set "JwtSettings:Issuer" "OneDealer.gr"
dotnet user-secrets set "JwtSettings:Audience" "test"
dotnet user-secrets set "JwtSettings:Key" "onedealerTokenKeyForDummyTest2025!"
dotnet user-secrets set "JwtSettings:ExpiryMinutes" "60"
