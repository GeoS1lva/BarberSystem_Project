using BarberSystem.Application.DTOs.Response;

namespace BarberSystem.Application.Interfaces.Queries
{
    public interface IUserQueries
    {
        public Task<List<ReturnUserResponse>?> GetAllUsers();
    }
}
