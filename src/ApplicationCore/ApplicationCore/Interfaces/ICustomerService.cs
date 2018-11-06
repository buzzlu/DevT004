using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICustomerService
    {

        IEnumerable<Customer> GetCustomer();
        Customer GetCustomer(Guid id);
        Task InsertCustomerAsync(Customer _customer);
        Task UpdateCustomer(Customer _customer);
        Task DeleteCustomerasync(Guid id);

    }
}