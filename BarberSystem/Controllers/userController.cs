 using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController(IUserAppService userAppService) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CreateUserRequest newUser)
        {
            var result = await userAppService.CreateAsync(newUser);

            if(result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpPatch("{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest user)
        {
            var result = await userAppService.UpdateAsync(user);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> ReturnUsers()
        {
            var result = await userAppService.GetAllUsersAsync();

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
