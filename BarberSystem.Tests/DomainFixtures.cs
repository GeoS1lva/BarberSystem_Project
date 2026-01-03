using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using BarberSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Tests
{
    public static class DomainFixtures
    {
        public static ResultPattern<Cpf> CreateValidCpf() => Cpf.Create("41829834088");
        public static ResultPattern<Email> CreateValidEmail() => Email.Create("test@test.com");
        public static ResultPattern<Password> CreateValidPassword()
        {
            string saltString = "vW7r8mP2vLq5A1wAb8de0A==";
            byte[] salt = Convert.FromBase64String(saltString);

            string hashString = "n5Kz9XW7r8mP2vLq5A1wAb8de0AcfD5D6eebF263f6Y=";
            byte[] hash = Convert.FromBase64String(hashString);
            var resultPassword = Password.Create(hash, salt);

            return resultPassword;
        }

        public static IdentitySystem CreateValidIdentity()
        {
            var resultEmail = CreateValidEmail();
            var resultPassword = CreateValidPassword();

            var resultIdentity = IdentitySystem.Create(resultEmail.Value, resultPassword.Value, Roles.administrator, ProfileType.user);

            return resultIdentity.Value;
        }

        public static User CreateValidUser()
        {
            var cpf = CreateValidCpf();
            var resultIdentity = CreateValidIdentity();

            var resultUser = User.Create("Geovana", "Silva", cpf.Value, "11111111111", resultIdentity);

            return resultUser.Value;
        }
    }
}
