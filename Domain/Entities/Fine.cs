namespace Domain.Entities
{
    public class Fine: AuditableEntity
    {
        public decimal Amount { get; set; }
        public int ClientId { get; set; }
        public int LoanId { get; set; }
        public string? Notes { get; set; }

        public Client Client { get; set; }
        public Loan Loan { get; set; }
        public Payment Payment { get; set; }
    }
}
