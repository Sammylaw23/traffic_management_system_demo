namespace TrafficManagementSystem.Application.DTOs.OffenceType
{
    public class NewOffenceTypeRequest
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Int16 Point { get; set; }
        public int Category { get; set; }
        public decimal FineAmount { get; set; }

    }
}
