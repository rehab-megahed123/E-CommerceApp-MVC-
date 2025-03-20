using E_CommerceApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Controllers
{
    public class RoleController : Controller
    {
        private  RoleManager<IdentityRole> RoleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole()
        {
            var roles = await RoleManager.Roles.ToListAsync();
            return View("AddRole",roles);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveRole(AddRoleVM role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = role.RoleName;
                var result = await RoleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {

                    return View("AddRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }

            return View("AddRole", role);

        }
    }
}
