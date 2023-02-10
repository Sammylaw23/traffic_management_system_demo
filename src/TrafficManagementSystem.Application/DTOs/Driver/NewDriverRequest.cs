using System.ComponentModel.DataAnnotations;

namespace TrafficManagementSystem.Application.DTOs.Driver
{
    public class NewDriverRequest
    {
        public Guid? Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? LicenseNo { get; set; }


        [Required]
        public char? Gender { get; set; }
    }
}
