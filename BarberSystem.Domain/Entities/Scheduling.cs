using BarberSystem.Domain.Common;
using BarberSystem.Domain.Enums;

namespace BarberSystem.Domain.Entities
{
    public class Scheduling : EntityBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public TimeOnly TotalServiceTime { get; private set; }
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public double TotalValue { get; private set; }
        public Status Status { get; private set; }

        public ICollection<SchedulingService> Services { get; private set; } = [];
        public ICollection<ServiceProvided> ServicesProvided { get; private set; } = [];

        private Scheduling(int userId, int customerId, TimeOnly totalServiceTime, DateTime startDateTime, DateTime endDateTime, double totalValue)
        {
            UserId = userId;
            CustomerId = customerId;
            TotalServiceTime = totalServiceTime;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            TotalValue = totalValue;
            Status = Status.pendente;
        }

        public static ResultPattern<Scheduling> Create(int userId, int customerId, DateTime startDateTime, DateTime endDateTime, TimeOnly totalServiceTime, double totalValue, List<ServiceProvided> services)
        {
            if (startDateTime.Date < DateTime.Now.Date)
                return ResultPattern<Scheduling>.Failure("Data de Agendamento Inválida!");

            var scheduling = new Scheduling(userId, customerId, totalServiceTime, startDateTime, endDateTime, totalValue);

            foreach (var service in services)
            {
                scheduling.AddService(service);
            }

            return ResultPattern<Scheduling>.Success(scheduling);
        }

        private void AddService(ServiceProvided service)
        {
            var schedulingService = new SchedulingService(0 ,service.Id, service.Value, service.ServiceTime);

            this.Services.Add(schedulingService);
        }

        protected Scheduling() { }
    }
}
