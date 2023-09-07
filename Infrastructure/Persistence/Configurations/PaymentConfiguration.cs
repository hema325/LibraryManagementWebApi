using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(f => f.Amount)
                .HasPrecision(9, 2);

            builder.HasOne(p => p.Client).WithMany().HasForeignKey(p => p.ClientId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Fine).WithOne(f => f.Payment).HasForeignKey<Payment>(p => p.FineId);
        }
    }
}
