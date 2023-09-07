namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
