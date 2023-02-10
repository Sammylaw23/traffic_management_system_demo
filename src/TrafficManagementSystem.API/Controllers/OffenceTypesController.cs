using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrafficManagementSystem.Application.DTOs.OffenceType;
using TrafficManagementSystem.Application.Interfaces.Services;

namespace TrafficManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/offencetypes")]
    [ApiController]
    public class OffenceTypesController : ControllerBase
    {
        private IOffenceTypeService _offenceTypeService;

        public OffenceTypesController(IOffenceTypeService offenceTypesService)
        {
            _offenceTypeService = offenceTypesService;
        }

        [HttpPost]
        public async Task<IActionResult> PostOffenceType(NewOffenceTypeRequest request)
        {
            await _offenceTypeService.SaveOffenceTypeAsync(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetOffenceTypes() => Ok(await _offenceTypeService.GetAllOffenceTypes());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffenceType([FromRoute] Guid id) => Ok(await _offenceTypeService.GetOffenceTypeById(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffenceType(Guid id)
        {
            await _offenceTypeService.DeleteOffenceTypeAsync(id);
            return NoContent();
        }

        [HttpGet("codes")]
        public async Task<IActionResult> GetOffenceTypeCodes()
            => Ok(await _offenceTypeService.GetOffenceTypeCodes());


    }
}
