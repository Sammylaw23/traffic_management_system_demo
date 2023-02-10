using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrafficManagementSystem.Application.DTOs.User;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Domain.Settings;

namespace TrafficManagementSystem.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
     
        private readonly IIdentityService _identityService;

        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(AuthenticationRequest request)
        {

            //sign token here
            var response = await _identityService.LoginAsync(request);
            return Ok(response);
        }
    }
}



