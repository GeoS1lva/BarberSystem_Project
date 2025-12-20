using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Infrastructure.Data.Context;
using BarberSystem.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Infrastructure.Data
{
    public class UnitOfWork(SqlServerDbContext context) : IUnitOfWork
    {
        public ISchedulingRepository SchedulingRepository { get; } = new SchedulingRepository(context);
        public IServiceProvidedRepository ServiceProvidedRepository { get; } = new ServiceProvidedRepository(context);
        public ISchedulingServiceRepository SchedulingServiceRepository { get; } = new SchedulingServiceRepository(context);
        public IIdentiySystemRepository IdentiySystemRepository { get; } = new IdentitySystemRepository(context);
        public IUserRepository UserRepository { get; } = new UserRepository(context);
        public ICustomerRepository CustomerRepository { get; } = new CustomerRepository(context);
        public IWorkScheduleRepository WorkScheduleRepository { get; } = new WorkScheduleRepository(context);

        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();
    }
}
