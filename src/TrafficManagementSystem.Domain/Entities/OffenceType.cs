namespace TrafficManagementSystem.Domain.Entities
{
    public class OffenceType : BaseEntity
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Int16 Point { get; set; }
        public int Category { get; set; }
        public decimal FineAmount { get; set; }

        //public virtual ICollection<Offence>? Offences { get; set; }
    }
}
