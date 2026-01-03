using BarberSystem.Application.DTOs.Request;
using BarberSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class schedulingController(ISchedulingAppService schedulingAppService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register(SchedulingRequest schedulingRequest)
        {
            var result = await schedulingAppService.CreateAsync(schedulingRequest);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpPatch("cancel/{Id}")]
        [Authorize]
        public async Task<IActionResult> Cancel(int Id)
        {
            var result = await schedulingAppService.CancelAsync(Id);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok();
        }

        [HttpGet("meus-agendamentos")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetAllMy()
        {
            var customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await schedulingAppService.GetAllByCustomer(customerId);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpGet("meus-atendimentos")]
        [Authorize(Roles = "administrator,user")]
        public async Task<IActionResult> GetAllMyScheduling()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await schedulingAppService.GetAllByUser(userId);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpGet("cliente/{clienteId}")]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> GetAllCustomer(int customerId)
        {
            var result = await schedulingAppService.GetAllByCustomer(customerId);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }

        [HttpGet("barbeiro/{barbeiroId}")]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> GetAllUser(int userId)
        {
            var result = await schedulingAppService.GetAllByUser(userId);

            if (result.Error)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Value);
        }
    }
}
