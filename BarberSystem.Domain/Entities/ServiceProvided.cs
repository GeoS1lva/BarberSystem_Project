using BarberSystem.Domain.Common;
using BarberSystem.Domain.Enums;

namespace BarberSystem.Domain.Entities
{
    public sealed class ServiceProvided : EntityBase
    {
        public string Name { get; private set; }
        public TimeOnly ServiceTime { get; private set; }
        public Category Category { get; private set; }
        public double Value { get; private set; }

        public ICollection<SchedulingService> Services { get; private set; } = [];
        public ICollection<Scheduling> Schedulings { get; private set; } = [];

        private ServiceProvided(string name, TimeOnly serviceTime, Category category, double value)
        {
            Name = name;
            ServiceTime = serviceTime;
            Category = category;
            Value = value;
        }

        public static ResultPattern<ServiceProvided> Create(string name, TimeOnly serviceTime, Category category, double value)
        {
            if (string.IsNullOrEmpty(name))
                return ResultPattern<ServiceProvided>.Failure("Nome não pode ser nulo!");

            if (name.Length < 3)
                return ResultPattern<ServiceProvided>.Failure("Nome precisa ter ao menos 3 caracteres");

            if (serviceTime < TimeOnly.Parse("00:10:00.0000"))
                return ResultPattern<ServiceProvided>.Failure("O serviço deve durar ao menos 10 minutos!");

            return ResultPattern<ServiceProvided>.Success(new ServiceProvided(name, serviceTime, category, value));
        }

        protected ServiceProvided() { }
    }
}
