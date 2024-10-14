using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Web.Data.Interfaces;

namespace Web.Data.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Product> Products {get; set;}

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductImage> Images {get; set;}

        public AppDbContext(DbContextOptions options) : base(options) 
        {
            
        }
    }
}
