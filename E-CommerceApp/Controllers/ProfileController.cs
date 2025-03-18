using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            

            return View("profile");
        }
    }
}
