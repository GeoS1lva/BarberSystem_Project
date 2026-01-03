using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using BarberSystem.Application.Interfaces.Security;
using BarberSystem.Domain.Common;
using BarberSystem.Domain.Interface.Repositories;
using BarberSystem.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Application.Services
{
    public class IdentitySystemAppService(IUnitOfWork unitOfWork, IIdentitySystemService identityService, ITokenService tokenService, IAuthCookieService authCookieService) : IIdentitySystemAppService
    {
        public async Task<ResultPattern> UpdateAsync(UpdateIdentitySystemRequest identity)
        {
            var identitySystem = await unitOfWork.IdentiySystemRepository.GetById(identity.Id);

            if (identitySystem is null)
                return ResultPattern.Failure("Identidade não encontrada");

            var resultIdentity = await identityService.UpdateEmailAsync(identitySystem, identity.NewEmail);

            if (resultIdentity.Error)
                return ResultPattern.Failure(resultIdentity.ErrorMessage);

            unitOfWork.IdentiySystemRepository.Update(identitySystem);
            await unitOfWork.SaveChangesAsync();

            return ResultPattern.Success();
        }

        public async Task<ResultPattern> LoginAsync(LoginRequest login)
        {
            var identity = await unitOfWork.IdentiySystemRepository.GetByEmail(login.Email);

            if (identity is null)
                return ResultPattern.Failure("Usuário não cadastrado!");

            var result = identityService.ValidateLogin(identity, login.Password);

            if(result.Error)
                return ResultPattern.Failure(result.ErrorMessage);

            var token = tokenService.TokeGenerator(identity, identity.Role.ToString());

            authCookieService.SetTokenCookie(token);

            return ResultPattern.Success();
        }
    }
}
