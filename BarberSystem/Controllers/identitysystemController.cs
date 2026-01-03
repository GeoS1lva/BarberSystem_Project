using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class identitysystemController(IIdentitySystemAppService identitySystemAppService) : ControllerBase
    {
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Update(UpdateIdentitySystemRequest request)
        {
            var result = await identitySystemAppService.UpdateAsync(request);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await identitySystemAppService.LoginAsync(request);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
