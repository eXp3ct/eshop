using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken, string activeTab = "Products")
        {
            var model = await _adminService.GetViewModel(activeTab, cancellationToken);

            return View(model);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            ViewBag.Categories = await _adminService.GetCategoriesList(cancellationToken: cancellationToken);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile Image, CancellationToken cancellationToken)
        {
            await _adminService.CreateProduct(model, Image, cancellationToken);

            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            var product = await _adminService.GetProductById(id, cancellationToken);

            if (product is null) return NotFound();

            ViewBag.Categories = await _adminService.GetCategoriesList(cancellationToken);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product newProduct, IFormFile Image, CancellationToken cancellationToken)
        {
            var product = await _adminService.EditProduct(newProduct, Image, cancellationToken);

            if (product is null) return NotFound();

            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _adminService.DeleteProduct(id, cancellationToken);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(string name, Guid? parentCategoryId, CancellationToken cancellationToken)
        {
            return Json(new { success = await _adminService.CreateCategory(name, parentCategoryId, cancellationToken) });
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Guid id, string newName, CancellationToken cancellationToken)
        {
            return Json(new { success = await _adminService.EditCategory(id, newName, cancellationToken) });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            await _adminService.DeleteCategory(id, cancellationToken);

            return RedirectToAction("Index", new { activeTab = "Categories" });
        }

    }
}
