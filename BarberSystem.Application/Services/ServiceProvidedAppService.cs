using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces;
using BarberSystem.Domain.Common;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;

namespace BarberSystem.Application.Services
{
    public class ServiceProvidedAppService(IServiceProvidedService serviceProvidedService, IUnitOfWork unitOfWork) : IServiceProvidedAppService
    {
        public async Task<ResultPattern<CreateServiceProvidedResponse>> CreateAsync(CreateServiceProvidedRequest newServiceProvided)
        {
            var result = await serviceProvidedService.CreateServiceProvided(newServiceProvided.Name, newServiceProvided.ServiceTime, newServiceProvided.Category, newServiceProvided.Value);

            if (result.Error)
                return ResultPattern<CreateServiceProvidedResponse>.Failure(result.ErrorMessage);

            unitOfWork.ServiceProvidedRepository.AddServiceProvided(result.Value);

            await unitOfWork.SaveChangesAsync();

            return ResultPattern<CreateServiceProvidedResponse>.Success(new CreateServiceProvidedResponse
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                ServiceTime = result.Value.ServiceTime,
                Category = result.Value.Category,
                Value = result.Value.Value
            });
        }
    }
}
