namespace TrafficManagementSystem.Application.DTOs.Offence
{
    public class OffenceDto
    {
        public Guid Id { get; set; }
        public int OffenceTypeId { get; set; }
        public string? OffenderName { get; set; }
        public string? OffenceTypeCode { get; set; }
        public string? PlateNumber { get; set; }
        public string? LicenseNo { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
