using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface IIdentiySystemRepository
    {
        public void AddIdentitySystem(IdentitySystem identitySystem);
        public Task<bool> ValidateIdentityId(int id);
        public Task<bool> ValidateIdentityEmail(string email);
        public Task<IdentitySystem?> GetById(int id);
        public void Update(IdentitySystem identitySystem);
        public Task<IdentitySystem?> GetByEmail(string email);
    }
}
