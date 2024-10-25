using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.DTOs;

namespace Inventory.Application.Commands
{
    public class UpdateCustomerCommand
    {
        public Guid CustomerId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public AddressDto Address { get; }

        public UpdateCustomerCommand(Guid customerId, string firstName, string lastName, string email, string phoneNumber, AddressDto address)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public class UpdateCustomerResult
        {
            public bool Success { get; set; }
            public IEnumerable<string> Errors { get; set; }
        }
    }
}
