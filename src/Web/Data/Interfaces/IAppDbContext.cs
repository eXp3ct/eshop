using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Interfaces
{
    public interface IAppDbContext : IDisposable
    {
        public DbSet<Product> Products { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<ProductImage> Images { get; }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
