using Inventory.API.Controllers;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Inventory.Application.Commands.CreateCustomerCommand;
using static Inventory.Application.Commands.DeleteCustomerCommand;
using static Inventory.Application.Commands.UpdateCustomerCommand;

namespace Inventory.UnitTests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new CustomerDto { /* Initialize with properties */ },
                new CustomerDto { /* Initialize with properties */ }
            };
            _mockCustomerService.Setup(service => service.GetAllCustomersAsync())
                                .ReturnsAsync(customers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCustomers = Assert.IsType<List<CustomerDto>>(okResult.Value);
            Assert.Equal(customers.Count, returnCustomers.Count);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerDto { /* Initialize with properties */ };
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(customerId))
                                .ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCustomer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal(customerId, returnCustomer.Id); // Assuming CustomerDto has an Id property
        }

        [Fact]
        public async Task GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(customerId))
                                .ReturnsAsync((CustomerDto)null);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            var customerDto = new CustomerDto { FirstName = "Test", LastName = "Test",Email = "test@test.com",Address=new AddressDto() { City = "tets" }, PhoneNumber="test" };
            var resultId = Guid.NewGuid();
            var result = new CreateCustomerResult { Success = true, CustomerId = resultId };
            _mockCustomerService.Setup(service => service.CreateCustomerAsync(customerDto))
                                .ReturnsAsync(result);

            // Act
            var resultAction = await _controller.CreateCustomer(customerDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(resultAction);
            Assert.Equal(resultId, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var customerDto = new CustomerDto { FirstName = "Test", LastName = "Test", Email = "test@test.com", Address = new AddressDto() { City = "tets" }, PhoneNumber = "test" };
            var result = new CreateCustomerResult { Success = false, Errors = new[] { "Error message" } };
            _mockCustomerService.Setup(service => service.CreateCustomerAsync(customerDto))
                                .ReturnsAsync(result);

            // Act
            var resultAction = await _controller.CreateCustomer(customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultAction);
            Assert.Equal(result.Errors, badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerDto = new CustomerDto { FirstName = "Test", LastName = "Test", Email = "test@test.com", Address = new AddressDto() { City = "tets" }, PhoneNumber = "test" };
            var result = new UpdateCustomerResult { Success = true };
            _mockCustomerService.Setup(service => service.UpdateCustomerAsync(customerId, customerDto))
                                .ReturnsAsync(result);

            // Act
            var resultAction = await _controller.UpdateCustomer(customerId, customerDto);

            // Assert
            Assert.IsType<NoContentResult>(resultAction);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerDto = new CustomerDto { FirstName = "Test", LastName = "Test", Email = "test@test.com", Address = new AddressDto() { City = "tets" }, PhoneNumber = "test" };
            var result = new UpdateCustomerResult { Success = false, Errors = new[] { "Error message" } };
            _mockCustomerService.Setup(service => service.UpdateCustomerAsync(customerId, customerDto))
                                .ReturnsAsync(result);

            // Act
            var resultAction = await _controller.UpdateCustomer(customerId, customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultAction);
            Assert.Equal(result.Errors, badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var result = new DeleteCustomerResult { Success = true };
            _mockCustomerService.Setup(service => service.DeleteCustomerAsync(customerId))
                                .ReturnsAsync(result);

            // Act
            var resultAction = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NoContentResult>(resultAction);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var result = new DeleteCustomerResult { Success = false, Errors = new[] { "Error message" } };
            _mockCustomerService.Setup(service => service.DeleteCustomerAsync(customerId))
                                .ReturnsAsync(result);

            // Act
            var resultAction = await _controller.DeleteCustomer(customerId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultAction);
            Assert.Equal(result.Errors, badRequestResult.Value);
        }
    }
}
