using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface ICustomerRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
