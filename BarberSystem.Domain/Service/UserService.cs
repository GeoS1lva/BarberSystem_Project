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
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        public async Task<ResultPattern<User>> CreateUser(string name, string surname, string cpf, string contact, IdentitySystem identitySystem)
        {
            var resultCpf = Cpf.Create(cpf);

            if (resultCpf.Error)
                return ResultPattern<User>.Failure(resultCpf.ErrorMessage);

            if (await unitOfWork.UserRepository.ValidateUserCpf(cpf))
                return ResultPattern<User>.Failure("Esse CPF já está cadastrado!");

            var resultUser = User.Create(name, surname, resultCpf.Value, contact, identitySystem);

            if (resultUser.Error)
                return ResultPattern<User>.Failure(resultUser.ErrorMessage);

            return ResultPattern<User>.Success(resultUser.Value);
        }
    }
}
