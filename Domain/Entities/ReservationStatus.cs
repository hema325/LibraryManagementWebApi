namespace Domain.Entities
{
    public class ReservationStatus: AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
