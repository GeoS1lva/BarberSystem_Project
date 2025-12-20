using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;

namespace BarberSystem.Domain.Service
{
    public class ServiceProvidedService(IUnitOfWork unitOfWork) : IServiceProvidedService
    {
        public async Task<ResultPattern<ServiceProvided>> CreateServiceProvided(string name, TimeOnly serviceTime, Category category, double value)
        {
            var resultValidate = await unitOfWork.ServiceProvidedRepository.GetByName(name);

            if (resultValidate)
                return ResultPattern<ServiceProvided>.Failure("Esse serviço já está cadastrado!");

            var resultService = ServiceProvided.Create(name, serviceTime, category, value);

            if (resultService.Error)
                return ResultPattern<ServiceProvided>.Failure(resultService.ErrorMessage);

            return ResultPattern<ServiceProvided>.Success(resultService.Value);
        }
    }
}
