using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberSystem.Infrastructure.Data.Queries
{
    public class UserQueries(SqlServerDbContext context) : IUserQueries
    {
        private readonly SqlServerDbContext _context = context;

        public async Task<List<ReturnUserResponse>?> GetAllUsers()
           => await _context.users
            .AsNoTracking()
           .Select(x => new ReturnUserResponse
           {
               Id = x.Id,
               Name = x.Name,
               Surname = x.Surname,
               Contact = x.Contact,
               HiringDate = x.HiringDate,
               DismissalDate = x.DismissalDate,
               StartTime = x.WorkSchedule.StartTime,
               EndTime = x.WorkSchedule.EndTime
           }).ToListAsync();
    }
}
