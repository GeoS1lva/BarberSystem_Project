using BarberSystem.Domain.Entities;
using BarberSystem.Domain.ValueObjects;

namespace BarberSystem.Tests.BarberSystem.Domain.Entities
{
    public class UserTests
    {
        [Theory]
        [InlineData("", "Silva", "11111111111", "Nome Inválido")]
        [InlineData("Ge", "Silva", "11111111111", "Nome Inválido")]
        [InlineData("Geovana", "", "11111111111", "Sobrenome Inválido")]
        [InlineData("Geovana", "Si", "11111111111", "Sobrenome Inválido")]
        [InlineData("Geovana", "Silva", "", "Contato Inválido")]
        [InlineData("Geovana", "Silva", "1111111111", "Contato Inválido")]
        public void Create_ShouldReturnError_WhenParametersAreInvalid(string name, string surname, string contact, string ExpectedError)
        {
            var cpf = DomainFixtures.CreateValidCpf();
            var identity = DomainFixtures.CreateValidIdentity();

            var userResult = User.Create(name, surname, cpf.Value, contact, identity);

            Assert.True(userResult.Error);
            Assert.Equal(ExpectedError, userResult.ErrorMessage);
        }
    }
}
