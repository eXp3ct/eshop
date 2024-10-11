using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Web.Data.Interfaces;
using Web.Models;
using Web.Utilities;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Category> _categories;
        private readonly IRepository<ProductImage> _images;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IRepository<Product> products, ILogger<AdminController> logger, IRepository<Category> categories, IRepository<ProductImage> images)
        {
            _products = products;
            _logger = logger;
            _categories = categories;
            _images = images;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken, string activeTab = "Products")
        {
            var model = new AdminPanelViewModel
            {
                Products = (IList<Product>)(await _products.GetListAsync(canecllationToken: cancellationToken) ?? throw new Exception("Cannot load products")),
                Categories = (IList<Category>)(await _categories.GetListAsync(canecllationToken: cancellationToken) ?? throw new Exception("Cannot load categories")),
                ActiveTab = activeTab
            };

            return View(model);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _categories.GetListAsync(canecllationToken: cancellationToken);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile Image, CancellationToken cancellationToken)
        {
            var image = new ProductImage
            {
                Id = Guid.NewGuid(),
                Extension = Image.FileName.Split('.').LastOrDefault(),
            };

            using var memStream = new MemoryStream();
            await Image.CopyToAsync(memStream, cancellationToken);
            image.Bytes = memStream.ToArray();

            
            // Получаем выбранные категории по их идентификаторам
            var selectedCategories = await _categories.GetListAsync(q => q.Where(c => model.CategoriesIds.Contains(c.Id)), canecllationToken: cancellationToken);
            _logger.LogWarning("Selected categories {categories}", selectedCategories.Count());

            model.Categories = [.. selectedCategories];
            model.Image = image;
            model.Article = ArticleGenerator.GenerateArticle(selectedCategories.FirstOrDefault()!.Name);

            await _images.AddAsync(image, cancellationToken);
            await _products.AddAsync(model, cancellationToken);
            await _products.SaveChangesAsync(cancellationToken);
            await _images.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            var product = await _products.GetByIdAsync(id, q => q.Include(x => x.Image).Include(x => x.Categories), cancellationToken);

            if (product is null)
                return NotFound();

            var categories = await _categories.GetListAsync(canecllationToken: cancellationToken);
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.Categories.Select(x => x.Id));

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product newProduct, IFormFile Image, CancellationToken cancellationToken)
        {
            var product = await _products.GetByIdAsync(newProduct.Id, q => q.Include(x => x.Image).Include(x => x.Categories), cancellationToken);

            if (product is null)
                return NotFound();

            var image = new ProductImage
            {
                Id = Guid.NewGuid(),
                Extension = Image.FileName.Split('.').LastOrDefault(),
            };

            using var memStream = new MemoryStream();
            await Image.CopyToAsync(memStream, cancellationToken);
            image.Bytes = memStream.ToArray();


            // Получаем выбранные категории по их идентификаторам
            var selectedCategories = await _categories.GetListAsync(q => q.Where(c => newProduct.CategoriesIds.Contains(c.Id)), cancellationToken);
            _logger.LogWarning("Selected categories {categories}", selectedCategories.Count());

            product.Categories = [.. selectedCategories];
            product.Image = image;
            product.Article = ArticleGenerator.GenerateArticle(selectedCategories.FirstOrDefault()!.Name);
            product.Id = newProduct.Id;
            product.Name = newProduct.Name;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;

            await _images.AddAsync(image, cancellationToken);
            await _images.SaveChangesAsync(cancellationToken);
            await _products.SaveChangesAsync(cancellationToken);

            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _products.DeleteAsync(id, cancellationToken);

            await _products.SaveChangesAsync(cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(string name, Guid parentCategoryId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var category = new Category { Name = name, ParentCategoryId = parentCategoryId };
                await _categories.AddAsync(category, cancellationToken);
                await _categories.SaveChangesAsync(cancellationToken);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Guid id, string newName, CancellationToken cancellationToken)
        {
            var category = await _categories.GetByIdAsync(id, canecllationToken: cancellationToken);
            if (category != null && !string.IsNullOrWhiteSpace(newName))
            {
                category.Name = newName;

                await _categories.SaveChangesAsync(cancellationToken);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            await _categories.DeleteAsync(id, cancellationToken);
            await _categories.SaveChangesAsync(cancellationToken);
            
            return RedirectToAction("Index", new { activeTab = "Categories" });
        }


    }
}
