using BarberSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Entities
{
    public class SchedulingService : EntityBase
    {
        public int SchedulingId { get; set; }

        public int ServiceProvidedId { get; set; }
        public ServiceProvided ServiceProvided { get; set; }

        public double PriceAtMoment { get; private set; }
        public TimeOnly DurationAtMoment { get; private set; }

        public SchedulingService(int schedulingId, ServiceProvided service, double price, TimeOnly duration)
        {
            SchedulingId = schedulingId;
            ServiceProvided = service;
            PriceAtMoment = price;
            DurationAtMoment = duration;
        }

        protected SchedulingService() { }
    }
}
