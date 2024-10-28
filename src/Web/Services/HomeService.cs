using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data.Interfaces;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;

        public HomeService(IRepository<Product> productsRepository, IRepository<Category> categoriesRepository)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<SelectList> GetCategoriesList(CancellationToken cancellationToken)
        {
            var categories = await _categoriesRepository.GetListAsync(canecllationToken: cancellationToken);

            return new SelectList(categories, "Id", "Name");
        }

        public async Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            return await _productsRepository.GetByIdAsync(
                id,
                q => q.Include(x => x.Image).Include(x => x.Categories),
                canecllationToken: cancellationToken);
        }

        public async Task<ProductFilterViewModel> GetProductFilterAsync(ProductFilterViewModel model, CancellationToken cancellationToken)
        {
            var query = (await _productsRepository
                    .GetListAsync(q => q
                        .Include(x => x.Image)          
                        .Include(x => x.Categories)     
                    , cancellationToken))?.AsQueryable();


            if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Count != 0)
            {
                query = query.Where(item => item.CategoriesIds.Any(c => model.SelectedCategoryIds.Contains(c)));
            }

            model.Items = [.. query];

            return model;
        }

        
    }
}
