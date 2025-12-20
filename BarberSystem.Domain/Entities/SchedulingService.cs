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

        public double PriceAtMoment { get; private set; }
        public TimeOnly DurationAtMoment { get; private set; }

        public SchedulingService(int schedulingId, int serviceId, double price, TimeOnly duration)
        {
            SchedulingId = schedulingId;
            ServiceProvidedId = serviceId;
            PriceAtMoment = price;
            DurationAtMoment = duration;
        }

        protected SchedulingService() { }
    }
}
