using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.DTOs.Response;
using BarberSystem.Application.Interfaces;
using BarberSystem.Application.Interfaces.Queries;
using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Services
{
    public class UserAppService(IUnitOfWork unitOfWork, IIdentitySystemService identityService, IUserService userService, IUserQueries userQueries) : IUserAppService
    {
        public async Task<ResultPattern<CreateUserResponse>> CreateAsync(CreateUserRequest user)
        {
            var resultIdentity = await identityService.CreateIdentity(user.Email, user.Password, user.Role, "user");

            if (resultIdentity.Error)
                return ResultPattern<CreateUserResponse>.Failure(resultIdentity.ErrorMessage);

            unitOfWork.IdentiySystemRepository.AddIdentitySystem(resultIdentity.Value);

            var resultUser = await userService.CreateUser(user.Name, user.Surname, user.Cpf, user.Contact, resultIdentity.Value);

            if (resultUser.Error)
                return ResultPattern<CreateUserResponse>.Failure(resultUser.ErrorMessage);

            unitOfWork.UserRepository.AddUser(resultUser.Value);

            var workSchuleUser = WorkSchedule.Create(resultUser.Value, user.StartTime, user.EndTime);

            if (workSchuleUser.Error)
                return ResultPattern<CreateUserResponse>.Failure(workSchuleUser.ErrorMessage);

            unitOfWork.WorkScheduleRepository.AddWorlSchedule(workSchuleUser.Value);

            await unitOfWork.SaveChangesAsync();

            return ResultPattern<CreateUserResponse>.Success(new CreateUserResponse
            {
                FullName = string.Concat(user.Name, " ", user.Surname)
            });
        }

        public async Task<ResultPattern<UpdateUserResponse>> UpdateAsync(UpdateUserRequest updateUser)
        {
            var user = await unitOfWork.UserRepository.GetById(updateUser.Id);

            var resultUser = user.Update(updateUser.Name, updateUser.Surname, updateUser.Contact);

            if (resultUser.Error)
                return ResultPattern<UpdateUserResponse>.Failure(resultUser.ErrorMessage);

            unitOfWork.UserRepository.Update(resultUser.Value);
            await unitOfWork.SaveChangesAsync();

            return ResultPattern<UpdateUserResponse>.Success(new UpdateUserResponse
            {
                Name = resultUser.Value.Name,
                Surname = resultUser.Value.Surname,
                Cpf = resultUser.Value.CPF.Value,
                Contact = resultUser.Value.Contact
            });
        }

        public async Task<ResultPattern> UpdateWorkScheduleAsync(UpdateWorkScheduleRequest updateUser)
        {
            var user = await unitOfWork.UserRepository.GetByIdWithWorkSchedule(updateUser.IdUser);

            var result = user.WorkSchedule.Update(updateUser.NewStartTime, updateUser.NewEndTime);

            if(result.Error)
                return ResultPattern.Failure(result.ErrorMessage);

            unitOfWork.UserRepository.Update(user);
            await unitOfWork.SaveChangesAsync();

            return ResultPattern.Success();
        }

        public async Task<ResultPattern<List<ReturnUserResponse>>> GetAllUsersAsync()
        {
            var resultUsers = await userQueries.GetAllUsers();

            if (resultUsers.Count == 0)
                return ResultPattern<List<ReturnUserResponse>>.Failure("Nenhum Usuário Encontrado!");

            return ResultPattern<List<ReturnUserResponse>>.Success(resultUsers);
        }
    }
}
