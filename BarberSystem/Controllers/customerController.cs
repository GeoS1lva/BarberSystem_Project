using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerController(ICustomerAppService customerAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(CreateCustomerRequest newCustomer)
        {
            var result = await customerAppService.CreateAsync(newCustomer);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
