using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class serviceprovidedController(IServiceProvidedAppService serviceProvidedAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(CreateServiceProvidedRequest newServiceProvided)
        {
            var result = await serviceProvidedAppService.CreateAsync(newServiceProvided);

            if(result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
