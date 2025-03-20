using System.Security.Claims;
using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    public class ProfileController : Controller
    {
       
        public IActionResult Profile()
        {
            var userName = User.Identity.Name;
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            UserProfileVM user = new UserProfileVM();
            user.userNmae = userName;
            user.Email = userEmail;
            return View("profile",user);
        }
    }
}
