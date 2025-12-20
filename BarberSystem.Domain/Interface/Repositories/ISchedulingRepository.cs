using BarberSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface ISchedulingRepository
    {
        public void AddScheduling(Scheduling scheduling);
        public Task<bool> ValidateSchedule(int userId, DateTime attemptStartDateTime, DateTime attemptEndDateTime);
    }
}
