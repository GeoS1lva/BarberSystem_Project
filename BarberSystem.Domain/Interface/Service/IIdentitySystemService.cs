using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;

namespace BarberSystem.Domain.Interface.Service
{
    public interface IIdentitySystemService
    {
        public Task<ResultPattern<IdentitySystem>> CreateIdentity(string email, string attempt, string role, string profileType);
        public Task<ResultPattern> UpdateEmailAsync(IdentitySystem identitySystem, string newEmail);
        public ResultPattern ValidateLogin(IdentitySystem identity, string attemptPassword);
    }
}
