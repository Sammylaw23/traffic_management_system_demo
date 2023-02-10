namespace TrafficManagementSystem.Application.DTOs.Offence
{
    public class NewOffenceRequest
    {
        public Guid? Id { get; set; }
        public string? OffenceTypeCode { get; set; }
        public string? OffenderName { get; set; }
        public string? PlateNumber { get; set; }
        public string? LicenseNo { get; set; }
    }
}
