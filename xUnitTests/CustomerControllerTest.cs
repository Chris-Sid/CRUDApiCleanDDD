using Microsoft.AspNetCore.Mvc;
using Moq;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities.Customers;
using MyApiProject.Infrastructure.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class CustomersControllerTests
    {
        private readonly CustomersController _controller;
        private readonly Mock<ICustomerRepository> _mockService;

        public CustomersControllerTests()
        {
            _mockService = new Mock<ICustomerRepository>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WithCustomerList()
        {
            var mockService = new Mock<ICustomerRepository>();
            mockService.Setup(s => s.GetCustomerAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Customer { name = "Test", email = "test@test.com" });

            var controller = new CustomersController(mockService.Object);

            // Act
            var result = await controller.GetCustomer(Guid.NewGuid());

            // Unwrap ActionResult<Customer>
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var customer = Assert.IsType<Customer>(okResult.Value);

            Assert.Equal("Test", customer.name);
        }
    }

}
