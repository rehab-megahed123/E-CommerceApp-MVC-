using E_commerce.DAl.Model;
using E_Commerce.Bll.Manager.Abstraction;
using E_CommerceApp.Models;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    public class CartController : Controller
    {
        private IProductManager ProductManager;
        public CartController(IProductManager productManager)
        {
            ProductManager = productManager;
        }
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // البحث عن المنتج في الكارت
            var existingItem = cart.FirstOrDefault(p => p.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++; // إذا كان المنتج موجودًا، زيدي الكمية
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = 1
                });
            }

            // حفظ الكارت بعد التعديل في Session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }
        [Authorize(Roles = "Customer")]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

    }
}
