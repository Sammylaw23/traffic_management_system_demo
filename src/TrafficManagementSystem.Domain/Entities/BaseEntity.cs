namespace TrafficManagementSystem.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
    }
}
