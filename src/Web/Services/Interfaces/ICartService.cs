using Web.Models;

namespace Web.Services.Interfaces
{
    public interface ICartService
    {
        public string SessionKey { get; set; }

        public CartViewModel AddToCart(Guid productId, string productName, decimal price, HttpContext context);
        public CartViewModel UpdateCart(Guid productId, int quantity, HttpContext context);
        public CartViewModel GetCart(HttpContext context);

    }
}
