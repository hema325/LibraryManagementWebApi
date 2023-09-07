namespace Domain.Entities
{
    public class Payment: AuditableEntity
    {
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public int ClientId { get; set; }
        public int FineId { get; set; }

        public Client Client { get; set; }
        public Fine Fine { get; set; }
    }
}
