using Domain.Models;

namespace Web.Models
{
    public class AdminPanelViewModel
    {
        public IList<Product> Products { get; set; } = [];
        public IList<Category> Categories { get; set; } = [];
        public string ActiveTab { get; set; } = "Products";
    }
}
