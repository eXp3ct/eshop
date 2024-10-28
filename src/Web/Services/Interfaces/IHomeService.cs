using Domain.Models;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IHomeService : ICategoriesFilter
    {
        public Task<ProductFilterViewModel> GetProductFilterAsync(ProductFilterViewModel model, CancellationToken cancellationToken);
        public Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    }
}
