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
    public class WorkScheduleRepository(SqlServerDbContext context) : IWorkScheduleRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void AddWorlSchedule(WorkSchedule workSchedule)
            => _context.workSchedules.Add(workSchedule);
    }
}
