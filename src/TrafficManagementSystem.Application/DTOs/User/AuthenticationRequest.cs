using System.ComponentModel.DataAnnotations;

namespace TrafficManagementSystem.Application.DTOs.User
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
