using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(ProductFilterViewModel model, CancellationToken cancellationToken)
        {
            ViewBag.Categories = await _homeService.GetCategoriesList(cancellationToken);

            var viewModel = await _homeService.GetProductFilterAsync(model, cancellationToken);

            return View(viewModel);
        }

        public async Task<IActionResult> Product(Guid id, CancellationToken cancellationToken)
        {
            var product = await _homeService.GetProductById(id, cancellationToken);

            if (product is null)
                return NotFound();

            return View(product);
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
