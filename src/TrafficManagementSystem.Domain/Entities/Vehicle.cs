namespace TrafficManagementSystem.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Colour { get; set; }
        public string? Brand { get; set; }
        public string? PlateNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? ChassisNo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Owner { get; set; }

    }
}
