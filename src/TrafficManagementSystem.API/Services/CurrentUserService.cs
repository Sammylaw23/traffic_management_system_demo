using System.Security.Claims;
using TrafficManagementSystem.Application.Interfaces.Services;

namespace TrafficManagementSystem.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal _currentUser;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = httpContextAccessor?.HttpContext?.User?? new ClaimsPrincipal();
        }

        public string? Username => _currentUser.FindFirstValue("username");
    }
}
