using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Queries
{
    public class GetCustomerByIdQuery
    {
        public Guid CustomerId { get; }

        public GetCustomerByIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
