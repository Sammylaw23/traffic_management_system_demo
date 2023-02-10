namespace TrafficManagementSystem.Application.DTOs.Vehicle
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Colour { get; set; }
        public string? Brand { get; set; }
        public string? PlateNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? ChassisNo { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? Owner { get; set; }
    }
}
