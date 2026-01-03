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
    public class IdentitySystemRepository(SqlServerDbContext context) : IIdentiySystemRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddIdentitySystem(IdentitySystem identitySystem)
            => _context.identitysSystem.Add(identitySystem);

        public async Task<bool> ValidateIdentityId(int id)
            => await _context.identitysSystem.AnyAsync(x => x.Id == id);

        public async Task<bool> ValidateIdentityEmail(string email)
            => await _context.identitysSystem.AnyAsync(x => x.Email.Value == email);

        public async Task<IdentitySystem?> GetById(int id)
            => await _context.identitysSystem.FirstOrDefaultAsync(x => x.Id == id);

        public void Update(IdentitySystem identitySystem)
            => _context.identitysSystem.Update(identitySystem);

        public async Task<IdentitySystem?> GetByEmail(string email)
            => await _context.identitysSystem
            .Include(x => x.Password)
            .FirstOrDefaultAsync(x => x.Email.Value == email);
    }
}
