using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Interfaces.Security
{
    public interface ITokenService
    {
        public string TokeGenerator(IdentitySystem identitySystem, string role, IEnumerable<Claim>? extraClaims = null);
    }
}
