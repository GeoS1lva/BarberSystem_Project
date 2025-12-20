using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Infrastructure.Data.Repositories
{
    public class CustomerRepository(SqlServerDbContext context) : ICustomerRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddCustomer(Customer customer)
            => _context.customers.Add(customer);

        public async Task<bool> ValidateCustomer(int customerId)
            => await _context.customers.AnyAsync(x => x.Id == customerId);

        public async Task<bool> ValidateCustomerCpf(string cpf)
            => await _context.customers.AnyAsync(x => x.CPF.Value == cpf);
        
        public async Task<Customer?> GetById(int id)
            => await _context.customers.FirstOrDefaultAsync(x => x.Id == id);
    }
}
