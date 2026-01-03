using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Domain.Common;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;

namespace BarberSystem.Application.Services
{
    public class CustomerAppService(IUnitOfWork unitOfWork, IIdentitySystemService identityService, ICustomerService customerService, ICustomerQueries customerQueries) : ICustomerAppService
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

        public async Task<ResultPattern<UpdateCustomerResponse>> UpdateAsync(UpdateCustomerRequest updateCustomer)
        {
            var customer = await unitOfWork.CustomerRepository.GetById(updateCustomer.Id);

            var resultCustomer = customer.Update(updateCustomer.Name, updateCustomer.Surname, updateCustomer.Contact);

            if (resultCustomer.Error)
                return ResultPattern<UpdateCustomerResponse>.Failure(resultCustomer.ErrorMessage);

            unitOfWork.CustomerRepository.Update(resultCustomer.Value);
            await unitOfWork.SaveChangesAsync();

            return ResultPattern<UpdateCustomerResponse>.Success(new UpdateCustomerResponse
            {
                Name = resultCustomer.Value.Name,
                Surname = resultCustomer.Value.Surname,
                Cpf = resultCustomer.Value.CPF.Value,
                Contact = resultCustomer.Value.Contact
            });
        }

        public async Task<ResultPattern<List<ReturnCustomerResponse>>> GetAllCustomersAsync()
        {
            var resultCustomers = await customerQueries.GetAllCustomers();

            if (resultCustomers.Count == 0)
                return ResultPattern<List<ReturnCustomerResponse>>.Failure("Nenhum Usuário Encontrado!");

            return ResultPattern<List<ReturnCustomerResponse>>.Success(resultCustomers);
        }
    }
}
