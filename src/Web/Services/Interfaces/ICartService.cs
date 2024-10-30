using Domain.Models;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface ICartService
    {
        public string SessionKey { get; set; }

        public CartViewModel AddToCart(CartItem item, HttpContext context);
        public CartViewModel UpdateCart(Guid productId, int quantity, HttpContext context);
        public CartViewModel GetCart(HttpContext context);

    }
}
