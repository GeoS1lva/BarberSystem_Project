using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Domain.Entities;
using BarberSystem.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberSystem.Infrastructure.Data.Queries
{
    public class SchedulingQueries(SqlServerDbContext context) : ISchedulingQueries
    {
        private readonly SqlServerDbContext _context = context;

        public async Task<List<SchedulingResponse>> GetAllBySchedulingCustomer(int customerId)
            => await _context.schedulings
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId)
            .Select(x => new SchedulingResponse
            {
                UserName = x.User.Name,
                CustomerName = x.Customer.Name,
                StartDateTime = x.StartDateTime,
                Services = x.Services.Select(s => new SchedulingServiceResponse
                {
                    Name = s.ServiceProvided.Name,
                    Value = s.PriceAtMoment
                }).ToList(),
                TotalValue = x.TotalValue
            }).ToListAsync();

        public async Task<List<SchedulingResponse>> GetAllBySchedulingUser(int userId)
            => await _context.schedulings
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new SchedulingResponse
            {
                UserName = x.User.Name,
                CustomerName = x.Customer.Name,
                StartDateTime = x.StartDateTime,
                Services = x.Services.Select(s => new SchedulingServiceResponse
                {
                    Name = s.ServiceProvided.Name,
                    Value = s.PriceAtMoment
                }).ToList(),
                TotalValue = x.TotalValue
            }).ToListAsync();
    }
}
