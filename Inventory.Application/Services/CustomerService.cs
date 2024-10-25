using Inventory.Application.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Inventory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inventory.Application.Commands.CreateCustomerCommand;

namespace Inventory.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            var customersDto = customers.Select(x => new CustomerDto {Email = x.Email, FirstName = x.FirstName,
                PhoneNumber = x.PhoneNumber,LastName = x.LastName, Address = new AddressDto { City = x.Address.City, PostalCode = x.Address.PostalCode,State = x.Address.State,Street= x.Address.Street}
            });

            return customersDto;
        }

        public async Task<CreateCustomerResult> CreateCustomerAsync(CustomerDto customerDto)
        {
            //var customer = new Customer()
            //{
            //    FirstName = customerDto.FirstName,
            //    LastName = customerDto.LastName,
            //    Email = customerDto.Email,
            //    PhoneNumber = customerDto.PhoneNumber,
            //    Address = new Address(customerDto.Address.Street, customerDto.Address.City, customerDto.Address.State, customerDto.Address.PostalCode)
            //};

            // var customer  =  new  Customer() { FirstName = customerDto.FirstName , LastName = customerDto.LastName, Email = customerDto.Email, PhoneNumber = customerDto.PhoneNumber, Address = new Address(customerDto.Address.Street, customerDto.Address.City, customerDto.Address.State, customerDto.Address.PostalCode) };

            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Address = new Address
                {
                    Street = customerDto.Address.Street,
                    City = customerDto.Address.City,
                    State = customerDto.Address.State,
                    PostalCode = customerDto.Address.PostalCode
                }
            };

          

            _customerRepository.AddCustomer(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync();

            return new CreateCustomerResult
            {
                Success = true,
                CustomerId = customer.Id
            };
        }

        public Task<DeleteCustomerCommand.DeleteCustomerResult> DeleteCustomerAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
                return null;

            return new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = new AddressDto
                {
                    Street = customer.Address.Street,
                    City = customer.Address.City,
                    State = customer.Address.State,
                    PostalCode = customer.Address.PostalCode
                }
            };
        }

        public Task<UpdateCustomerCommand.UpdateCustomerResult> UpdateCustomerAsync(Guid customerId, CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }
    }
}
