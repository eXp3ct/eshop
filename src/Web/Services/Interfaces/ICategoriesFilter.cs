using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Services.Interfaces
{
    public interface ICategoriesFilter
    {
        public Task<SelectList> GetCategoriesList(CancellationToken cancellationToken);
    }
}
