using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data.Interfaces;
using Web.Models;
using Web.Services.Interfaces;
using Web.Utilities;

namespace Web.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<ProductImage> _productsImageRepository;

        public AdminService(
            IRepository<Product> productsRepository,
            IRepository<Category> categoriesRepository,
            IRepository<ProductImage> productsImageRepository)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesRepository;
            _productsImageRepository = productsImageRepository;
        }

        public async Task<bool> CreateCategory(string categoryName, Guid? parentCategoryId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                var category = new Category { Name = categoryName, ParentCategoryId = parentCategoryId };
                await _categoriesRepository.AddAsync(category, cancellationToken);
                await _categoriesRepository.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<Product> CreateProduct(Product model, IFormFile file, CancellationToken cancellationToken)
        {
            var selectedCategories = await _categoriesRepository.GetListAsync(
                q => q.Where(c => model.CategoriesIds.Contains(c.Id)),
                canecllationToken: cancellationToken);

            model.Categories = [.. selectedCategories];
            model.Image = await SaveImage(file, cancellationToken);
            model.Article = ArticleGenerator.GenerateArticle(selectedCategories!.FirstOrDefault()!.Name);

            var added = await _productsRepository.AddAsync(model, cancellationToken);
            await _productsRepository.SaveChangesAsync(cancellationToken);

            return added;
        }

        public async Task<bool> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            await _categoriesRepository.DeleteAsync(categoryId, cancellationToken);
            await _categoriesRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            await _productsRepository.DeleteAsync(id, cancellationToken);

            await _productsRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> EditCategory(Guid id, string newName, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepository.GetByIdAsync(id, canecllationToken: cancellationToken);
            if (category != null && !string.IsNullOrWhiteSpace(newName))
            {
                category.Name = newName;

                await _categoriesRepository.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<Product?> EditProduct(Product model, IFormFile file, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetByIdAsync(model.Id,
                                                                 q => q.Include(x => x.Image).Include(x => x.Categories),
                                                                 cancellationToken);

            if (product is null)
                return null;

            var selectedCategories = await _categoriesRepository.GetListAsync(q => q.Where(c => model.CategoriesIds.Contains(c.Id)), cancellationToken);

            product.Categories = [.. selectedCategories];
            product.Image = await SaveImage(file, cancellationToken);
            product.Article = ArticleGenerator.GenerateArticle(selectedCategories!.FirstOrDefault()!.Name);
            product.Id = model.Id;
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;

            await _productsRepository.SaveChangesAsync(cancellationToken);

            return product;
        }

        public async Task<SelectList> GetCategoriesList(CancellationToken cancellationToken)
        {
            var categories = await _categoriesRepository.GetListAsync(canecllationToken: cancellationToken);

            return new SelectList(categories, "Id", "Name");
        }

        public async Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            return await _productsRepository.GetByIdAsync(id, q => q.Include(x => x.Image).Include(x => x.Categories), cancellationToken);
        }

        public async Task<AdminPanelViewModel> GetViewModel(string activeTab, CancellationToken cancellationToken)
        {
            return new AdminPanelViewModel
            {
                ActiveTab = activeTab,
                Categories = (IList<Category>)(await _categoriesRepository.GetListAsync(canecllationToken: cancellationToken)
                    ?? throw new Exception("Cannot load categories")),
                Products = (IList<Product>)(await _productsRepository.GetListAsync(canecllationToken: cancellationToken) 
                    ?? throw new Exception("Cannot load products"))
            };
        }

        private async Task<ProductImage> SaveImage(IFormFile file, CancellationToken cancellationToken)
        {
            var image = new ProductImage
            {
                Id = Guid.NewGuid(),
                Extension = file.FileName.Split('.').LastOrDefault(),
            };

            using var memStream = new MemoryStream();
            await file.CopyToAsync(memStream, cancellationToken);
            image.Bytes = memStream.ToArray();

            await _productsImageRepository.AddAsync(image, cancellationToken);
            await _productsImageRepository.SaveChangesAsync(cancellationToken);

            return image;
        }
    }
}
