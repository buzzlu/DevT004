using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAsyncRepository<Customer> _customerAsyncRepository;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(
                    IRepository<Customer> customerRepository,
                    IAsyncRepository<Customer> customerAsyncRepository)
        {
            _customerRepository = customerRepository;
            _customerAsyncRepository = customerAsyncRepository;
        }


        public async Task DeleteCustomerasync(Guid id)
        {
            var customer = await _customerAsyncRepository.GetByIdAsync(id);

            await _customerAsyncRepository.DeleteAsync(customer);
        }

        public IEnumerable<Customer> GetCustomer()
        {
            return _customerRepository.ListAll();
        }

        public Customer GetCustomer(Guid id)
        {
            var customer = _customerRepository.GetById(id);
            return customer;
        }

        public async Task InsertCustomerAsync(Customer _customer)
        {
            await _customerAsyncRepository.AddAsync(_customer);
        }

        public async Task UpdateCustomer(Customer _customer)
        {
            await _customerAsyncRepository.UpdateAsync(_customer);
        }
    }
}
