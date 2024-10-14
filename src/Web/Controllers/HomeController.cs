using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Data.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Category> _categories;
        private readonly IRepository<Product> _product;

        public HomeController(ILogger<HomeController> logger, IRepository<Product> product, IRepository<Category> categories)
        {
            _logger = logger;
            _product = product;
            _categories = categories;
        }

        public async Task<IActionResult> Index(ProductFilterViewModel model, CancellationToken cancellationToken)
        {
            var allCategories = await _categories.GetListAsync(canecllationToken: cancellationToken);
            ViewBag.Categories = new MultiSelectList(allCategories, "Id", "Name", model.SelectedCategoryIds);

            var query = (await _product.GetListAsync(q => q.Include(x => x.Categories)))!.AsQueryable();

            if(model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
            {
                query = query.Where(item => item.Categories.Any(c => model.SelectedCategoryIds.Contains(c.Id)));
            }

            model.Items = query.ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
