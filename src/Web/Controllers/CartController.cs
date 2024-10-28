﻿using Microsoft.AspNetCore.Mvc;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";

        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _cartService.SessionKey = CartSessionKey;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart(HttpContext);
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid productId, string productName, decimal price)
        {
            _cartService.AddToCart(productId, productName, price, HttpContext);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateCart(Guid productId, int quantity)
        {
            _cartService.UpdateCart(productId, quantity, HttpContext);
            return RedirectToAction("Index");
        }

    }
}
