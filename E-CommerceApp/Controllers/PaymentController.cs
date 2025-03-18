using System.Security.Claims;
using E_commerce.DAl.Model;
using E_CommerceApp.Models;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace E_CommerceApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PaymentController(IConfiguration configuration, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"]; // ✅ استخدام المفتاح السري
        }

        public IActionResult Checkout()
        {
            var cart = GetCartItems(); // ✅ جلب سلة التسوق
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart"); // إذا كانت السلة فارغة، الرجوع لصفحة السلة
            }

            var totalAmount = (int)(cart.Sum(item => item.Price * item.Quantity) * 100); // ✅ حساب الإجمالي بالدولار * 100 (للسنت)

            var model = new CheckoutViewModel
            {
                StripePublishableKey = _configuration["Stripe:PublishableKey"],
                Amount = totalAmount
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ProcessPayment()
        {
            var cart = GetCartItems();
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart"); // تأكيد وجود عناصر في السلة
            }

            int totalAmount = (int)(cart.Sum(item => item.Price * item.Quantity) * 100); // ✅ حساب السعر الإجمالي

            var domain = $"{Request.Scheme}://{Request.Host}/";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cart.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (int)(item.Price * 100), // ✅ حساب السعر الصحيح لكل منتج
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName
                        }
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "Payment/Success",
                CancelUrl = domain + "Payment/Cancel"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }

        public async Task<IActionResult> Success()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            // Get the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or wherever you want to redirect if no user is logged in
            }

            // Calculate total amount for the order
            decimal totalAmount = cart.Sum(item => item.Price * item.Quantity);

            // Create the Order entity
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = OrderStatus.Completed, // Set initial status as Pending
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId=item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            // Save the Order to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Clear the cart after successful order
            HttpContext.Session.SetObjectAsJson("Cart", new List<CartItem>());

            return View(order); // Optionally pass the order to the view for confirmation
        }

        public IActionResult Cancel()
        {
            return View();
        }

        // ✅ استرجاع سلة التسوق من الجلسة أو قاعدة البيانات
        private List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            return cart ?? new List<CartItem>();
        }
    }
}