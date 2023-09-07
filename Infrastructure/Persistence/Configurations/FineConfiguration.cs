using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.Property(f => f.Amount)
                .HasPrecision(9, 2);

            builder.HasOne(f => f.Client).WithMany().HasForeignKey(f => f.ClientId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(f => f.Loan).WithMany().HasForeignKey(f => f.LoanId);
        }
    }
}
