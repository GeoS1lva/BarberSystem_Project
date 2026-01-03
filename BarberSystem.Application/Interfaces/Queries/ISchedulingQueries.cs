using BarberSystem.Application.DTOs.Response;

namespace BarberSystem.Application.Interfaces.Queries
{
    public interface ISchedulingQueries
    {
        public Task<List<SchedulingResponse>> GetAllBySchedulingCustomer(int customerId);
        public Task<List<SchedulingResponse>> GetAllBySchedulingUser(int userId);
    }
}
