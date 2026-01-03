using BarberSystem.Domain.Common;
using BarberSystem.Domain.Entities;
using BarberSystem.Domain.Enums;
using BarberSystem.Domain.Interface;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;
using BarberSystem.Domain.ValueObjects;

namespace BarberSystem.Domain.Service
{
    public class IdentitySystemService(IPasswordService passwordService, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : IIdentitySystemService
    {
        public async Task<ResultPattern<IdentitySystem>> CreateIdentity(string email, string attempt, string role, string profileType)
        {
            var resultEmail = Email.Create(email);

            if (resultEmail.Error)
                return ResultPattern<IdentitySystem>.Failure(resultEmail.ErrorMessage);

            if (await unitOfWork.IdentiySystemRepository.ValidateIdentityEmail(email))
                return ResultPattern<IdentitySystem>.Failure("Esse e-mail já está cadastrado");

            var resultPassword = passwordService.GeneratePassword(attempt);

            if (resultPassword.Error)
                return ResultPattern<IdentitySystem>.Failure(resultPassword.ErrorMessage);

            if (!Enum.IsDefined(typeof(Roles), role))
                return ResultPattern<IdentitySystem>.Failure("Papel do usuário não identificado!");

            var resultRole = Enum.Parse<Roles>(role);

            if (!Enum.IsDefined(typeof(ProfileType), profileType))
                return ResultPattern<IdentitySystem>.Failure("Tipo de Perfil não identificado!");

            var resultProfileType = Enum.Parse<ProfileType>(profileType);

            var resultIdenitySystem = IdentitySystem.Create(resultEmail.Value, resultPassword.Value, resultRole, resultProfileType);

            if (resultIdenitySystem.Error)
                return ResultPattern<IdentitySystem>.Failure(resultIdenitySystem.ErrorMessage);

            return ResultPattern<IdentitySystem>.Success(resultIdenitySystem.Value);
        }

        public async Task<ResultPattern> UpdateEmailAsync(IdentitySystem identitySystem, string newEmail)
        {
            var resultEmail = Email.Create(newEmail);

            if (resultEmail.Error)
                return ResultPattern.Failure(resultEmail.ErrorMessage);

            if (await unitOfWork.IdentiySystemRepository.ValidateIdentityEmail(newEmail))
                return ResultPattern.Failure("Esse e-mail já está cadastrado");

            identitySystem.UpdateEmail(resultEmail.Value);

            return ResultPattern.Success();
        }

        public ResultPattern ValidateLogin(IdentitySystem identity, string attemptPassword)
        {
            var result = passwordHasher.ValidatePasswordInternal(attemptPassword, identity.Password.HashPassword, identity.Password.SaltPassword);

            if (result == false)
                return ResultPattern.Failure("Senha Incorreta!");

            return ResultPattern.Success();
        }
    }
}
