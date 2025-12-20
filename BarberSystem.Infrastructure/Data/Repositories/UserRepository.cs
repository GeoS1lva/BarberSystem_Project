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
    public class UserRepository(SqlServerDbContext context) : IUserRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddUser(User user)
            => _context.users.Add(user);

        public async Task<bool> ValidateUser(int userId)
            => await _context.users.AnyAsync(x => x.Id == userId);

        public async Task<bool> ValidateUserCpf(string cpf)
            => await _context.users.AnyAsync(x => x.CPF.Value == cpf);

        public async Task<User?> GetById(int id)
            => await _context.users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
