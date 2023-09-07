namespace Domain.Entities
{
    public class Client: AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public int GenderId { get; set; }
        public int StatusId { get; set; }
        public ClientStatus Status { get; set; }
        public Gender Gender { get; set; }
    }
}
