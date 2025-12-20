using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Infrastructure.Data.Repositories
{
    public class SchedulingServiceRepository(SqlServerDbContext context) : ISchedulingServiceRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddSchedulingService(SchedulingService schedulingService)
            => _context.schedulingServices.Add(schedulingService);

        
    }
}
