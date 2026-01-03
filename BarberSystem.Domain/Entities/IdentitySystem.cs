using BarberSystem.Domain.Common;
using BarberSystem.Domain.Enums;
using BarberSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Entities
{
    public sealed class IdentitySystem : EntityBase
    {
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public Roles Role { get; private set; }
        public ProfileType ProfileType { get; private set; }

        public Customer? Customer { get; set; }
        public User? User { get; set; }

        private IdentitySystem(Email email, Password password, Roles role, ProfileType profileType)
        {
            Email = email;
            Password = password;
            Role = role;
            ProfileType = profileType;
        }

        public static ResultPattern<IdentitySystem> Create(Email email, Password password, Roles role, ProfileType profileType)
        {
            if (role == Roles.administrator && profileType == ProfileType.client)
                return ResultPattern<IdentitySystem>.Failure("Cliente pode somente ter perfil de Cliente!");

            if (role == Roles.user && profileType == ProfileType.client)
                return ResultPattern<IdentitySystem>.Failure("Cliente pode somente ter perfil de Cliente!");

            if (role == Roles.client && profileType != ProfileType.client)
                return ResultPattern<IdentitySystem>.Failure("Cliente pode somente ter perfil de Cliente!");

            return ResultPattern<IdentitySystem>.Success(new IdentitySystem(email, password, role, profileType));
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        protected IdentitySystem() { }
    }
}
