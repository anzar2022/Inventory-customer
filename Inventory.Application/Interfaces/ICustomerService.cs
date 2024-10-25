using Inventory.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inventory.Application.Commands.CreateCustomerCommand;
using static Inventory.Application.Commands.DeleteCustomerCommand;
using static Inventory.Application.Commands.UpdateCustomerCommand;

namespace Inventory.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(Guid customerId);
        Task<CreateCustomerResult> CreateCustomerAsync(CustomerDto customerDto);
        Task<UpdateCustomerResult> UpdateCustomerAsync(Guid customerId, CustomerDto customerDto);
        Task<DeleteCustomerResult> DeleteCustomerAsync(Guid customerId);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    }
}
