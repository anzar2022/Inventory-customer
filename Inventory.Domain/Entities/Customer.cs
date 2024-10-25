using Inventory.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public Address Address { get;  set; }

      //  public Customer() { }

        //public Customer(string firstName, string lastName, string email, string phoneNumber, Address address)
        //{
        //    Id = Guid.NewGuid();
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    PhoneNumber = phoneNumber;
        //    Address = address;
        //}
    }
}
