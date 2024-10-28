using Domain.Models;

namespace Web.Services.Interfaces
{
    public interface IProductAdminPanel
    {
        public Task<Product> CreateProduct(Product model, IFormFile file, CancellationToken cancellationToken);
        public Task<Product?> EditProduct(Product model, IFormFile file, CancellationToken cancellationToken);
        public Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken);
    }
}
