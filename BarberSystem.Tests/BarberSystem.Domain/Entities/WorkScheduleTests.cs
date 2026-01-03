using BarberSystem.Domain.Entities;

namespace BarberSystem.Tests.BarberSystem.Domain.Entities
{
    public static class ServiceTestTime
    {
        public static TheoryData<TimeOnly, TimeOnly, string> InvalidTime =>
        new TheoryData<TimeOnly, TimeOnly, string>
        {
            {TimeOnly.Parse("07:59:00"), TimeOnly.Parse("18:00:00"), "Colaborador inicia antes do horário de expediente!" },
            {TimeOnly.Parse("08:00:00"), TimeOnly.Parse("20:00:00"), "Colaborador sai após o fim do expediente!" }

        };
    }

    public class WorkScheduleTests
    {
        [Theory]
        [MemberData(nameof(ServiceTestTime.InvalidTime), MemberType = typeof(ServiceTestTime))]
        public void Create_ShouldReturnError_WhenParametersAreInvalid(TimeOnly startTime, TimeOnly endTime, string errorMessage)
        {
            var resultWorkSchedule = WorkSchedule.Create(DomainFixtures.CreateValidUser(), startTime, endTime);

            Assert.True(resultWorkSchedule.Error);
            Assert.Equal(errorMessage, resultWorkSchedule.ErrorMessage);
        }
    }
}
