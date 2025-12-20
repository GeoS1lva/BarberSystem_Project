using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;
using BarberSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Domain.Service
{
    public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
    {
        public async Task<ResultPattern<Customer>> CreateCustomer(string name, string surname, string cpf, string contact, IdentitySystem identitySystem)
        {
            var resultCpf = Cpf.Create(cpf);

            if (resultCpf.Error)
                return ResultPattern<Customer>.Failure(resultCpf.ErrorMessage);

            var result = await unitOfWork.CustomerRepository.ValidateCustomerCpf(cpf);

            if (result)
                return ResultPattern<Customer>.Failure("Esse CPF já está cadastrado!");

            var resultCustomer = Customer.Create(name, surname, resultCpf.Value, contact, identitySystem);

            if(resultCustomer.Error)
                return ResultPattern<Customer>.Failure(resultCustomer.ErrorMessage);

            return ResultPattern<Customer>.Success(resultCustomer.Value);
        }
    }
}
