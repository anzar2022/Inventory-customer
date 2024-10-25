using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    public class DeleteCustomerCommand
    {
        public Guid CustomerId { get; }

        public DeleteCustomerCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
        public class DeleteCustomerResult
        {
            public bool Success { get; set; }
            public IEnumerable<string> Errors { get; set; }
        }
    }
}
