using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Web.Data.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepository<Product> _products;
        private readonly IRepository<Category> _categories;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IRepository<Product> products, ILogger<AdminController> logger, IRepository<Category> categories)
        {
            _products = products;
            _logger = logger;
            _categories = categories;
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
