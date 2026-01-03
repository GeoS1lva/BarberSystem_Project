using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberSystem.Infrastructure.Data.Repositories
{
    public class SchedulingRepository(SqlServerDbContext context) : ISchedulingRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddScheduling(Scheduling scheduling)
            => _context.schedulings.Add(scheduling);

        public async Task<bool> ValidateSchedule(int userId, DateTime attemptStartDateTime, DateTime attemptEndDateTime)
            => await _context.schedulings
            .AsNoTracking()
            .AnyAsync(x => x.UserId == userId &&
            x.Status != Status.cancelado &&
            x.StartDateTime < attemptEndDateTime &&
            x.EndDateTime > attemptStartDateTime);

        public async Task<Scheduling?> GetById(int id)
            => await _context.schedulings.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Scheduling>> GetExpiredAsync()
            => await _context.schedulings
            .Where(x => x.StartDateTime.Date == DateTime.Now.Date && x.EndDateTime.TimeOfDay >= DateTime.Now.TimeOfDay && x.Status == Status.pendente)
            .ToListAsync();
    }
}
