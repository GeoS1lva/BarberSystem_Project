using BarberSystem.Domain.Common;
using BarberSystem.Domain.ValueObjects;

namespace BarberSystem.Domain.Interface.Service
{
    public interface IPasswordService
    {
        public ResultPattern<Password> GeneratePassword(string attempt);
    }
}
