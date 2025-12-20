using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class schedulingController(ISchedulingAppService schedulingAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(SchedulingRequest schedulingRequest)
        {
            var result = await schedulingAppService.CreateAsync(schedulingRequest);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
