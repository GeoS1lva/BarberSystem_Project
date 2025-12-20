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
    public class ServiceProvidedRepository(SqlServerDbContext context) : IServiceProvidedRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddServiceProvided(ServiceProvided serviceProvided)
            => _context.servicesProvideds.Add(serviceProvided);

        public async Task<List<ServiceProvided>?> GetByIds(List<int> ids)
            => await _context.servicesProvideds.Where(s => ids.Contains(s.Id)).ToListAsync();

        public async Task<bool> GetByName(string name)
            => await _context.servicesProvideds.AnyAsync(s => s.Name == name);
    }
}
