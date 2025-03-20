using System.Security.Claims;
using E_commerce.DAl.Model;
using E_commerce.DAl.Repository;
using E_Commerce.Bll.Manager.Abstraction;
using E_CommerceApp.Models;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    public class OrderController : Controller
    {
       private IOrderManager OrderManager;
        private readonly ApplicationDbContext _context;


        public OrderController(IOrderManager OrderManager, ApplicationDbContext _context)
        {
            this.OrderManager = OrderManager;
            this._context = _context;

        }

        public IActionResult ShowAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res=OrderManager.ShowAll(userId);
            return View("ShowOrder", res);
            
        }
        public IActionResult NotNow()
        {
            // استرجاع العناصر من السلة من الـ Session
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            // حساب المبلغ الإجمالي للطلب
            var totalAmount = cartItems.Sum(item => item.Quantity * item.Price);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or wherever you want to redirect if no user is logged in
            }
            // إنشاء كائن طلب جديد
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = OrderStatus.Pending, // Set initial status as Pending
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            // إضافة الطلب إلى قاعدة البيانات
            _context.Orders.Add(order);
            _context.SaveChanges();

           

            

            // حذف العناصر من الـ Session بعد إنشاء الطلب
            HttpContext.Session.Remove("CartItems");

            // توجيه المستخدم إلى صفحة النجاح أو تفاصيل الطلب
            return RedirectToAction("ShowAll");
        }
        [HttpPost]
        public IActionResult Cancle(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("ShowAll");
        }
        [HttpPost]
        public IActionResult CancleAll(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Show");
        }
        public async Task< IActionResult> Show()
        {
            var res = await OrderManager.Show();
            return View(res);
        }
    }
}
