using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get;  set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string PostalCode { get;  set; }

        //public Address(string street, string city, string state, string postalCode)
        //{
        //    Street = street;
        //    City = city;
        //    State = state;
        //    PostalCode = postalCode;
        //}
    }
}
