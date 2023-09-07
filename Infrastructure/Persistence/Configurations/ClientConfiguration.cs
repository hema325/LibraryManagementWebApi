using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(256);

            builder.HasIndex(c => c.Name)
                .IsUnique(true);

            builder.OwnsMany(c => c.PhoneNumbers, builder =>
            {
                builder.Property(ph => ph.Value).HasMaxLength(24);
            });

            builder.HasOne(c => c.Status).WithMany().HasForeignKey(c => c.StatusId);
            builder.HasOne(c => c.Gender).WithMany().HasForeignKey(c => c.GenderId);
        }
    }
}
