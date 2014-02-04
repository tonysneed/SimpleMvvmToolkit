using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMvvm_Portable_Library
{
    public class MockCustomerServiceAgent : ICustomerServiceAgent
    {
        // Create a fake customer
        public Customer CreateCustomer()
        {
            return new Customer
            {
                CustomerId = 1,
                CustomerName = "John Doe",
                City = "Dallas"
            };
        }
    }
}
