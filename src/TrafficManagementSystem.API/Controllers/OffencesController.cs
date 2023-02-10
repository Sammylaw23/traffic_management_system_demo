using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrafficManagementSystem.Application.DTOs.Offence;
using TrafficManagementSystem.Application.Interfaces.Services;

namespace TrafficManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/offences")]
    [ApiController]
    public class OffencesController : ControllerBase
    {
        private IOffenceService _offenceService;

        public OffencesController(IOffenceService offenceService)
        {
            _offenceService = offenceService;
        }

        [HttpPost]
        public async Task<IActionResult> PostOffence(NewOffenceRequest request)
        {
            await _offenceService.SaveOffenceAsync(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOffences() => Ok(await _offenceService.GetAllOffences());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffence([FromRoute] Guid id) => Ok(await _offenceService.GetOffenceById(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffence(Guid id)
        {
            await _offenceService.DeleteOffenceAsync(id);
            return NoContent();
        }

    }
}
