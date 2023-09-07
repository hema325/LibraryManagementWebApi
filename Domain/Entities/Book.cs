namespace Domain.Entities
{
    public class Book: AuditableEntity
    {
        public string Name { get; set; }
        public int OwnedQuantity { get; set; }
        public DateTime ReleasedAt { get; set; }
        public string? Notes { get; set; }
        public List<Image> Images { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public List<Author> Authors { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
