namespace Domain.Entities
{
    public class Reservation: AuditableEntity
    {
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public int StatusId { get; set; }
        public string? Notes { get; set; }

        public Book Book { get; set; }
        public Client Client { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
