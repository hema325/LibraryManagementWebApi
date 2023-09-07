using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name)
                .HasMaxLength(256);

            builder.HasIndex(b => b.Name)
                .IsUnique(true);

            builder.HasOne(b => b.Category).WithMany().HasForeignKey(b => b.CategoryId);
            builder.HasMany(b => b.Authors).WithMany();

            builder.OwnsMany(b => b.Images, builder =>
            {
                builder.Property(i => i.Path).HasMaxLength(260);
                builder.HasIndex(i => i.Path).IsUnique(true);
            });
        }
    }
}
