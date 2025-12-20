using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Infrastructure.Data.Repositories
{
    public class SchedulingRepository(SqlServerDbContext context) : ISchedulingRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddScheduling(Scheduling scheduling)
            => _context.schedulings.Add(scheduling);

        public async Task<bool> ValidateSchedule(int userId, DateTime attemptStartDateTime, DateTime attemptEndDateTime)
            => _context.schedulings
            .AsNoTracking()
            .Any(x => x.UserId == userId &&
            x.Status != Status.cancelado &&
            x.StartDateTime < attemptEndDateTime &&
            x.EndDateTime > attemptStartDateTime);

    }
}
