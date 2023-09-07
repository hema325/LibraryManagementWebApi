namespace Domain.Entities
{
    public class Author: AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public int GenderId { get; set; }

        public Gender Gender { get; set; }
    }
}
