using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(256);

            builder.HasIndex(a => a.Name)
                .IsUnique(true);

            builder.HasOne(a => a.Gender).WithMany().HasForeignKey(a => a.GenderId);
        }
    }
}
