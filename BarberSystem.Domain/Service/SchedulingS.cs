using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;

namespace BarberSystem.Domain.Service
{
    public class SchedulingS(IUnitOfWork unitOfWork) : ISchedulingS
    {
        public async Task<ResultPattern<Scheduling>> AddSchedule(int userId, int customerId, DateTime startDateTime, List<ServiceProvided> services)
        {
            var totalServiceTime = TimeOnly.MinValue;
            double totalValue = 0;

            foreach (var service in services)
            {
                totalServiceTime = totalServiceTime.Add(service.ServiceTime.ToTimeSpan());
                totalValue += service.Value;
            }

            DateTime endDateTime = startDateTime.Add(totalServiceTime.ToTimeSpan());

            if (await unitOfWork.SchedulingRepository.ValidateSchedule(userId, startDateTime, endDateTime))
                return ResultPattern<Scheduling>.Failure("Esse horário não está disponível");

            var result = Scheduling.Create(userId, customerId, startDateTime, endDateTime, totalServiceTime, totalValue, services);

            if (result.Error)
                return ResultPattern<Scheduling>.Failure(result.ErrorMessage);

            return ResultPattern<Scheduling>.Success(result.Value);
        }
    }
}
