using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface ICustomerRepository
    {
        public void AddCustomer(Customer customer);
        public Task<bool> ValidateCustomer(int customerId);
        public Task<bool> ValidateCustomerCpf(string cpf);
        public Task<Customer?> GetById(int id);
        public void Update(Customer customer);
    }
}
