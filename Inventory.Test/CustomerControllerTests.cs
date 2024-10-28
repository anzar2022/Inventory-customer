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

namespace Inventory.Test
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
        public async Task GetAll_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var expectedCustomers = new List<CustomerDto>
            {
                new CustomerDto { Id = Guid.NewGuid(), FirstName = "Test Customer 1" },
                new CustomerDto { Id = Guid.NewGuid(), FirstName = "Test Customer 2" }
            };

            _mockCustomerService.Setup(service => service.GetAllCustomersAsync())
                .ReturnsAsync(expectedCustomers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomers = Assert.IsType<List<CustomerDto>>(okResult.Value);
            Assert.Equal(expectedCustomers.Count, returnedCustomers.Count);
        }

        [Fact]
        public async Task GetCustomerById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedCustomer = new CustomerDto
            {
                Id = customerId,
                FirstName = "Test Customer"
            };

            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(expectedCustomer);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal(expectedCustomer.Id, returnedCustomer.Id);
        }

        [Fact]
        public async Task GetCustomerById_WithInvalidId_ReturnsNotFound()
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
        public async Task CreateCustomer_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                FirstName = "New Customer",
                Email = "test@example.com"
            };

            var expectedResult = new Application.Commands.CreateCustomerCommand.CreateCustomerResult
            {
                Success = true,
                CustomerId = Guid.NewGuid()
            };

            _mockCustomerService.Setup(service => service.CreateCustomerAsync(customerDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CreateCustomer(customerDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(CustomerController.GetCustomerById), createdAtActionResult.ActionName);
            Assert.Equal(expectedResult.CustomerId, createdAtActionResult.Value);
        }

        [Fact]
        public async Task CreateCustomer_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var customerDto = new CustomerDto();
            var expectedResult = new Application.Commands.CreateCustomerCommand.CreateCustomerResult
            {
                Success = false,
                Errors = new List<string> { "Validation error" }
            };

            _mockCustomerService.Setup(service => service.CreateCustomerAsync(customerDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CreateCustomer(customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedResult.Errors, badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateCustomer_WithValidData_ReturnsNoContent()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerDto = new CustomerDto
            {
                FirstName = "Updated Customer"
            };

            var expectedResult = new Application.Commands.UpdateCustomerCommand.UpdateCustomerResult { Success = true };

            _mockCustomerService.Setup(service => service.UpdateCustomerAsync(customerId, customerDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerDto = new CustomerDto();
            var expectedResult = new Application.Commands.UpdateCustomerCommand.UpdateCustomerResult
            {
                Success = false,
                Errors = new List<string> { "Validation error" }
            };

            _mockCustomerService.Setup(service => service.UpdateCustomerAsync(customerId, customerDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedResult.Errors, badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteCustomer_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedResult = new Application.Commands.DeleteCustomerCommand.DeleteCustomerResult { Success = true };

            _mockCustomerService.Setup(service => service.DeleteCustomerAsync(customerId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedResult = new Application.Commands.DeleteCustomerCommand.DeleteCustomerResult
            {
                Success = false,
                Errors = new List<string> { "Customer not found" }
            };

            _mockCustomerService.Setup(service => service.DeleteCustomerAsync(customerId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedResult.Errors, badRequestResult.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task CreateCustomer_WithInvalidName_ReturnsBadRequest(string invalidName)
        {
            // Arrange
            var customerDto = new CustomerDto { FirstName = invalidName };
            var expectedResult = new Application.Commands.CreateCustomerCommand.CreateCustomerResult
            {
                Success = false,
                Errors = new List<string> { "Name is required" }
            };

            _mockCustomerService.Setup(service => service.CreateCustomerAsync(customerDto))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CreateCustomer(customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expectedResult.Errors, badRequestResult.Value);
        }
    }
}
