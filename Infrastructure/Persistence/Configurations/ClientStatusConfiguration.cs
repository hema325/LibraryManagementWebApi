using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ClientStatusConfiguration : IEntityTypeConfiguration<ClientStatus>
    {
        public void Configure(EntityTypeBuilder<ClientStatus> builder)
        {
            builder.Property(cs => cs.Name)
                .HasMaxLength(256);

            builder.HasIndex(cs => cs.Name)
                .IsUnique(true);
        }
    }
}
