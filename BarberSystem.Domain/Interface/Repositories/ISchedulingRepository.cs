using BarberSystem.Domain.Entities;

namespace BarberSystem.Domain.Interface.Repositories
{
    public interface ISchedulingRepository
    {
        public void AddScheduling(Scheduling scheduling);
        public Task<bool> ValidateSchedule(int userId, DateTime attemptStartDateTime, DateTime attemptEndDateTime);
        public Task<Scheduling?> GetById(int id);
        public Task<List<Scheduling>> GetExpiredAsync();
    }
}
