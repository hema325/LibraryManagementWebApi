namespace Domain.Entities
{
    public class ClientStatus: AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
