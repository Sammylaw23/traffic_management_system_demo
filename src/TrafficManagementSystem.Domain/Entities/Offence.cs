namespace TrafficManagementSystem.Domain.Entities
{
    public class Offence : BaseEntity
    {
        //public Guid OffenceTypeId { get; set; } //I don't think it is needed. I think OffenceTypeCode can be used instead
        public string? OffenceTypeCode { get; set; }
        public string? OffenderName { get; set; }
        public string? PlateNumber { get; set; }
        public string? LicenseNo { get; set; }

        //public virtual OffenceType? OffenceType { get; set; }
    }
}
