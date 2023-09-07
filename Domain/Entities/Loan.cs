namespace Domain.Entities
{
    public class Loan: AuditableEntity
    {
        public string? Notes { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Book Book { get; set; }
        public Client Client { get; set; }
    }
}
