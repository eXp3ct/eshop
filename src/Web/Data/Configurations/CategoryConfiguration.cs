using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ParentCategory)
                .WithMany(x => x.ChildrenCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Products)
               .WithMany(x => x.Categories)
               .UsingEntity(j => j.ToTable("ProductCategories"));
        }
    }
}
