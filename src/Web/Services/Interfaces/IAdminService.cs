using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IAdminService : IProductAdminPanel, ICategoriesAdminPanel, ICategoriesFilter
    {
        public Task<AdminPanelViewModel> GetViewModel(string activeTab, CancellationToken cancellationToken);
        public Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    }
}
