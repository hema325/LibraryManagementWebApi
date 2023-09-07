using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Author> Authors { get; }
        DbSet<Book> Books { get; }
        DbSet<Gender> Genders { get; }
        DbSet<ClientStatus> ClientStatuses { get; }
        DbSet<Client> Clients { get; }
        DbSet<ReservationStatus> ReservationStatuses { get; }
        DbSet<Reservation> Reservations { get; }
        DbSet<Loan> Loans { get; }
        DbSet<Fine> Fines { get; }
        DbSet<Payment> Payments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
