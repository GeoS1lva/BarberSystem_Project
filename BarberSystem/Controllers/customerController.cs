using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using BarberSystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerController(ICustomerAppService customerAppService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "administrator, client")]
        public async Task<IActionResult> Register(CreateCustomerRequest newCustomer)
        {
            var result = await customerAppService.CreateAsync(newCustomer);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpPatch("{Id}")]
        [Authorize(Roles = "administrator, client")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequest customer)
        {
            var result = await customerAppService.UpdateAsync(customer);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize(Roles = "administrator, user")]
        public async Task<IActionResult> ReturnCustomers()
        {
            var result = await customerAppService.GetAllCustomersAsync();

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
