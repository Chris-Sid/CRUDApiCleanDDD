using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Moq.Protected;
using MyApiProject.Application.DTOs;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities;
using MyApiProject.Infrastructure.Data;
using MyApiProject.Infrastructure.Helpers;
using MyApiProject.Infrastructure.Helpers.Response;
using MyApiProject.Infrastructure.Services.Address;
using StackExchange.Redis;
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
            // Mock JwtTokenGenerator
            var mockJwt = new Mock<IJwtTokenGenerator>();
            mockJwt.Setup(j => j.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>()))
                   .Returns("mocked-jwt-token");

            var cancellationToken = CancellationToken.None;
            var token = mockJwt.Object.GenerateJwtToken("Tester", "password");

            var Headers = new AddressRequestHeaders
            {
                Authorization = "Bearer " + token,
                RequestId = "mock"
            };

            var originalAddress = MockAddressData.Addresses[0];
            var UpdatedAddressDtoObject = originalAddress.Clone();
            UpdatedAddressDtoObject.AltLanguageCity = "MAROUSI TEST POST";

            // Mock HttpClient behavior
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)) // won't be hit in mock
                .Verifiable();

            var mockHttpClient = new HttpClient(mockHttpHandler.Object);
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient);

            var baseUrl = "https://localhost:7093/swagger/index.html";
            var service = new AddressService(mockHttpClientFactory.Object, baseUrl);

            //  Act
            var result = await service.PostAddressAsync(UpdatedAddressDtoObject, Headers, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UpdatedAddressDtoObject.AltLanguageCity, result.AltLanguageCity);
            Assert.Equal(originalAddress.AddressId.InternalId, result.AddressId.InternalId);
            Assert.NotEqual(originalAddress.AltLanguageCity, result.AltLanguageCity);

            // Confirm no real HTTP call was made
            mockHttpHandler.Protected().Verify(
                "SendAsync",
                Times.Never(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }

}
