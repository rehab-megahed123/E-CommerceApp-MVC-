using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.ViewComponents
{
    public class AddCategoryViewComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
