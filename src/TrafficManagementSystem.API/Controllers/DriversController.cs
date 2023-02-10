using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrafficManagementSystem.Application.DTOs.Driver;
using TrafficManagementSystem.Application.Interfaces.Services;

namespace TrafficManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost]
        public async Task<IActionResult> PostDriver(NewDriverRequest request)
        {
            await _driverService.SaveDriverAsync(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetDrivers() => Ok(await _driverService.GetAllDrivers());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver([FromRoute] Guid id) => Ok(await _driverService.GetDriverById(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            await _driverService.DeleteDriverAsync(id);
            return NoContent();
        }
    }
}
