using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price).HasPrecision(18, 6);
            builder.HasOne(x => x.Image)
                .WithOne(x => x.Product)
                .HasForeignKey<Product>(x => x.ImageId);
            builder.HasMany(x => x.Categories)
                .WithMany(x => x.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));

        }
    }
}
