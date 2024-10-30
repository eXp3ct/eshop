using Domain.Models;
using Web.Extensions;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class CartService : ICartService
    {
        public string SessionKey { get; set; } = string.Empty;

        public CartViewModel AddToCart(CartItem item, HttpContext context)
        {
            var cart = GetCart(context);
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                item.Quantity = 1;
                cart.Items.Add(item);
            }
            SaveToCart(cart, context);
            return cart;
        }

        public CartViewModel GetCart(HttpContext context)
        {
            var cart = context.Session.GetObjectFromJson<CartViewModel>(SessionKey);
            return cart ?? new CartViewModel();
        }

        public void SaveToCart(CartViewModel cartViewModel, HttpContext context)
        {
            context.Session.SetObjectAsJson(SessionKey, cartViewModel);
        }

        public CartViewModel UpdateCart(Guid productId, int quantity, HttpContext context)
        {
            var cart = GetCart(context);
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
            }

            SaveToCart(cart, context);

            return cart;
        }
    }
}
