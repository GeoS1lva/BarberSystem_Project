using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface IUnitOfWork
    {
        public ISchedulingRepository SchedulingRepository { get; }
        public IServiceProvidedRepository ServiceProvidedRepository { get; }
        public ISchedulingServiceRepository SchedulingServiceRepository { get; }
        public IIdentiySystemRepository IdentiySystemRepository { get; }
        public IUserRepository UserRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IWorkScheduleRepository WorkScheduleRepository { get; }

        public Task SaveChangesAsync();
    }
}
