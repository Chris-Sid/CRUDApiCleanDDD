if the user logs in (username: OneDealer Password: OneDealer2025! ) 
then the JWT Token is returned which is used in each request in the X-Authorization Header
Since the there are no real data 
I put mock data (MockAddressData.cs)  for GET Address (RequestId: mock ,"internalId": "int1",
"externalId": "ext1" Authorization : JWT token) and it returns mock data accordingly in Post it changes the Data if a Mock is tampered with
Finally I did a dummy xUnit testing in Post Address Service.
