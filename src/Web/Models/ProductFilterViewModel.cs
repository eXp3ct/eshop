using Domain.Models;

namespace Web.Models
{
    public class ProductFilterViewModel
    {
        public List<Product> Items { get; set; } = new List<Product>();
        public List<Guid> SelectedCategoryIds { get; set; } = new List<Guid>();
    }
}
