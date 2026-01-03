using BarberSystem.Domain.Interface.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BarberSystem.Infrastructure.Services.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class UpdateSchedulingJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UpdateSchedulingJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var schedulingExpired = await unitOfWork.SchedulingRepository.GetExpiredAsync();

                if (schedulingExpired.Count == 0)
                    return;

                foreach (var scheduling in schedulingExpired)
                {
                    scheduling.CompletedScheduling();
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
