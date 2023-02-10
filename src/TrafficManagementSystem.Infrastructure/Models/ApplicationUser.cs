using Microsoft.AspNetCore.Identity;

namespace TrafficManagementSystem.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; internal set; }
    }
}
