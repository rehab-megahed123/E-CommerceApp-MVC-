using E_Commerce.Bll.Manager.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private IProductManager ProductManager;
        public ProductViewComponent(IProductManager productManager)
        {
            ProductManager = productManager;
        }

        
            public async Task<IViewComponentResult> InvokeAsync()
            {

           var res=await ProductManager.GetAllAsync();
                return View(res);
            }
            
        
    }
}
