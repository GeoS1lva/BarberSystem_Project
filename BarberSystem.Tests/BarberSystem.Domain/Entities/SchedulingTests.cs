using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Tests.BarberSystem.Domain.Entities
{
    public class SchedulingTests
    {
        [Fact]
        public void Create_ShouldReturnError_WhenParametersAreInvalid()
        {
            DateTime startDateTime = DateTime.Today.AddDays(-1);
            string errorMessage = "Data de Agendamento Inválida!";

            var resultScheduling = Scheduling.Create(1, 1, startDateTime, DateTime.Now, TimeOnly.MinValue, 1, new List<ServiceProvided>());

            Assert.True(resultScheduling.Error);
            Assert.Equal(errorMessage, resultScheduling.ErrorMessage);
        }
    }
}
