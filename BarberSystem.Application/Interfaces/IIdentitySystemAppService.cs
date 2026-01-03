using BarberSystem.Application.DTOs.Request;
using BarberSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Interfaces
{
    public interface IIdentitySystemAppService
    {
        public Task<ResultPattern> UpdateAsync(UpdateIdentitySystemRequest user);
        public Task<ResultPattern> LoginAsync(LoginRequest login);
    }
}
