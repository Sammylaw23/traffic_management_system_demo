namespace TrafficManagementSystem.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? LicenseNo { get; set; }
        public char Gender { get; set; }
    }
}
