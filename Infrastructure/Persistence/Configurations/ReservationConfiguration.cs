using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.Book).WithMany().HasForeignKey(r => r.BookId);
            builder.HasOne(r => r.Client).WithMany().HasForeignKey(r => r.ClientId);
            builder.HasOne(r => r.Status).WithMany().HasForeignKey(r => r.StatusId);
        }
    }
}
