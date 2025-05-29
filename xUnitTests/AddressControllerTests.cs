using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Moq.Protected;
using MyApiProject.API.Controllers;
using MyApiProject.Application.DTOs;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities;
using MyApiProject.Infrastructure.Data;
using MyApiProject.Infrastructure.Helpers;
using MyApiProject.Infrastructure.Helpers.Response;
using MyApiProject.Infrastructure.Services.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class AddressControllerTests
    {

        [Fact]
        public async Task PostAddress_ReturnsOk_WhenValidAddressIsPosted()
        {
            // Arrange
            var mockService = new Mock<IAddressService>();
            var cancellationToken = CancellationToken.None;
            var jwtGenerator = new JwtTokenGenerator();
            var Token = jwtGenerator.GenerateJwtToken("Tester");
            
            var Headers = new AddressRequestHeaders()
            {
                Authorization = "Bearer" + Token,
                RequestId= "mock"
            };

            var originalAddress = MockAddressData.Addresses[0]; // Use one of the mocked addresses
            var UpdatedAddressDtoObject = originalAddress.Clone();
            UpdatedAddressDtoObject.AltLanguageCity = "MAROUSI TEST POST";

            // Mock HttpMessageHandler to prevent real HTTP calls
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)) // Will not be called in mock path
                .Verifiable();

            // Create a mock HttpClient
            var mockHttpClient = new HttpClient(mockHttpHandler.Object);

            // Mock IHttpClientFactory
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient);

            var baseUrl = "https://localhost:7093/swagger/index.html";
            var service = new AddressService(mockHttpClientFactory.Object, baseUrl);


            // Act
            var result = await service.PostAddressAsync(UpdatedAddressDtoObject, Headers, cancellationToken);

            // Assert
            Assert.NotNull(result);
            if (string.Equals(UpdatedAddressDtoObject.AltLanguageCity, result.AltLanguageCity, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Updated Address Value: {UpdatedAddressDtoObject.AltLanguageCity}");
                Console.WriteLine($"Actual Address Value: {result.AltLanguageCity}");
                Console.WriteLine("Expected Value (from assertion): Alt Mock City");
            }

            Assert.Equal(UpdatedAddressDtoObject.AltLanguageCity, result.AltLanguageCity);
            Assert.Equal(originalAddress.AddressId.InternalId, result.AddressId.InternalId);
            Assert.Equal(originalAddress.AltLanguageCity, result.AltLanguageCity); //should be false in case post works fine without exceptions

            // HTTP call should not happen due to "mock" logic
            mockHttpHandler.Protected().Verify(
                "SendAsync",
                Times.Never(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
