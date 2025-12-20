using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController(IUserAppService userAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRequest newUser)
        {
            var result = await userAppService.CreateAsync(newUser);

            if(result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
