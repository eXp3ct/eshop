using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Web.Data.Interfaces;

namespace Web.Data.Contexts
{
    public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
    {
        public DbSet<Product> Products {get; set;}

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductImage> Images {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var parentCategoryId = Guid.NewGuid();
            var parentCategoryId2 = Guid.NewGuid();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Category>().HasData(
            [
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Клавиатуры",
                    ParentCategoryId = parentCategoryId
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Наушники",
                    ParentCategoryId = parentCategoryId
                },
                new Category 
                {
                    Id = parentCategoryId,
                    Name = "Компьютерная переферия",
                },
                new Category
                {
                    Id = parentCategoryId2,
                    Name = "Комплектующие пк",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Видеокарты",
                    ParentCategoryId = parentCategoryId2
                }
            ]);
        }
    }
}
