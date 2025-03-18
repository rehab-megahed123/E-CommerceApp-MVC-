using System.Reflection.Metadata.Ecma335;
using E_Commerce.Bll.Manager.Abstraction;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_CommerceApp.ViewComponents
{
    public class AddProductViewComponent :ViewComponent
    {
        private ICategoryManager CategoryManager;
        public AddProductViewComponent(ICategoryManager categoryManager)
        {
            CategoryManager = categoryManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AddProductVM addproduct = new AddProductVM();
            //addproduct.CategoryList = await CategoryManager.GetAll();

           // addproduct.CategoryOptions = new SelectList(addproduct.CategoryList, "Id", "Name");
            return View(addproduct);
        }
    }
}
