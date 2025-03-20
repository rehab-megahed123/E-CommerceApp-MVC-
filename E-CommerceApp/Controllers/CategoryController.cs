using E_commerce.DAl.Model;
using E_Commerce.Bll.Manager.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryManager CategoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            CategoryManager = categoryManager;
               
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SaveAddCategory(Category category)
        {
            CategoryManager.AddCategory(category);
            CategoryManager.SaveContext();
            return View("MainScreen");


        }
    }
}
