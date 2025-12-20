using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces;
using BarberSystem.Domain.Common;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;

namespace BarberSystem.Application.Services
{
    public class CustomerAppService(IUnitOfWork unitOfWork, IIdentitySystemService identityService, ICustomerService customerService) : ICustomerAppService
    {
        public async Task<ResultPattern<CreateCustomerResponse>> CreateAsync(CreateCustomerRequest customer)
        {
            var resultIdentity = await identityService.CreateIdentity(customer.Email, customer.Password, "client", "client");

            if (resultIdentity.Error)
                return ResultPattern<CreateCustomerResponse>.Failure(resultIdentity.ErrorMessage);

            unitOfWork.IdentiySystemRepository.AddIdentitySystem(resultIdentity.Value);

            var resultCustomer = await customerService.CreateCustomer(customer.Name, customer.Surname, customer.Cpf, customer.Contact, resultIdentity.Value);

            if (resultCustomer.Error)
                return ResultPattern<CreateCustomerResponse>.Failure(resultCustomer.ErrorMessage);

            unitOfWork.CustomerRepository.AddCustomer(resultCustomer.Value);

            await unitOfWork.SaveChangesAsync();

            return ResultPattern<CreateCustomerResponse>.Success(new CreateCustomerResponse
            {
                FullName = string.Concat(customer.Name, " ", customer.Surname)
            });
        }
    }
}
