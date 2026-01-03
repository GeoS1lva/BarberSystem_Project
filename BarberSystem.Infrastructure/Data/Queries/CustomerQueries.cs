using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Infrastructure.Data.Queries
{
    public class CustomerQueries(SqlServerDbContext context) : ICustomerQueries
    {
        private readonly SqlServerDbContext _context = context;

        public async Task<List<ReturnCustomerResponse>?> GetAllCustomers()
           => await _context.customers
            .AsNoTracking()
           .Select(x => new ReturnCustomerResponse
           {
               Id = x.Id,
               Name = x.Name,
               Surname = x.Surname,
               Contact = x.Contact,
               RegistrationDate = x.RegistrationDate
           }).ToListAsync();
    }
}
