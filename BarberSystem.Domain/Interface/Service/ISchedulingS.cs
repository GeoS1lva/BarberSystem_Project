using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Service
{
    public interface ISchedulingS
    {
        public Task<ResultPattern<Scheduling>> AddSchedule(int userId, int customerId, DateTime startDateTime, List<ServiceProvided> services);
    }
}
