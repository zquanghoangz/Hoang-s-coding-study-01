using System.Collections.Generic;

namespace BillingSystem.Tests
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Customers { get; set; }
    }
}