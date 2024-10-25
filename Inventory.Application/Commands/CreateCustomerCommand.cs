using Inventory.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    public class CreateCustomerCommand
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public AddressDto Address { get; }

        public CreateCustomerCommand(string firstName, string lastName, string email, string phoneNumber, AddressDto address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public class CreateCustomerResult
        {
            public bool Success { get; set; }
            public Guid CustomerId { get; set; }
            public IEnumerable<string> Errors { get; set; }
        }
    }
}
