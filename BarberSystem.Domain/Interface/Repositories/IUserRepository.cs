using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface IUserRepository
    {
        public void AddUser(User user);
        public Task<bool> ValidateUser(int userId);
        public Task<bool> ValidateUserCpf(string cpf);
        public Task<User?> GetById(int id);
        public Task<User?> GetByIdWithWorkSchedule(int id);
        public void Update(User user);
    }
}
