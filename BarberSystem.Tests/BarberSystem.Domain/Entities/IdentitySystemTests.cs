using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Tests.BarberSystem.Domain.Entities
{
    public class IdentitySystemTests
    {
        [Theory]
        [InlineData(Roles.administrator, ProfileType.client, "Cliente pode somente ter perfil de Cliente!")]
        [InlineData(Roles.user, ProfileType.client, "Cliente pode somente ter perfil de Cliente!")]
        [InlineData(Roles.client, ProfileType.user, "Cliente pode somente ter perfil de Cliente!")]
        public void Create_ShouldReturnError_WhenParametersAreInvalid(Roles role, ProfileType profileType, string ExpectedError)
        {
            var email = DomainFixtures.CreateValidEmail();
            var password = DomainFixtures.CreateValidPassword();

            var identityResult = IdentitySystem.Create(email.Value, password.Value, role, profileType);

            Assert.True(identityResult.Error);
            Assert.Equal(ExpectedError, identityResult.ErrorMessage);
        }
    }
}
