using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;

namespace BarberSystem.Tests.BarberSystem.Domain.Entities
{
    public class ServiceProvidedTests
    {
        public static class ServiceTestData
        {
            public static TheoryData<string, TimeOnly, string> InvalidData =>
                new TheoryData<string, TimeOnly, string>
                {
                    {"Ge", TimeOnly.Parse("00:15:00"), "Nome Inválido!" },
                    {"Geovana", TimeOnly.Parse("00:05:00"), "O serviço deve durar ao menos 10 minutos!" }
                };
        }

        [Theory]
        [MemberData(nameof(ServiceTestData.InvalidData), MemberType = typeof(ServiceTestData))]
        public void Create_ShouldReturnError_WhenParametersAreInvalid(string name, TimeOnly serviceTime, string errorMessage)
        {
            var resultServiceProvided = ServiceProvided.Create(name, serviceTime, Category.sobrancelha, 15.00);

            Assert.True(resultServiceProvided.Error);
            Assert.Equal(errorMessage, resultServiceProvided.ErrorMessage);
        }
    }
}
