using E_commerce.DAl.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using E_CommerceApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using E_commerce.DAl.Migrations;
using E_CommerceApp.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;

namespace E_CommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> Manager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> manager,
            SignInManager<ApplicationUser> signIn,
            ApplicationDbContext context
        )
        {
            Manager = manager;
            SignInManager = signIn;
            _context = context;
        }

        public IActionResult SelectRole()
        {
            return View("SelectRole");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FullName = register.FullName,
                    UserName = register.FullName,
                    Email = register.email,
                    PasswordHash = register.Password,
                    UserType = register.Role
                };

                // Save to the database
                IdentityResult result = await Manager.CreateAsync(user, register.Password);

                // Cookie authentication
                if (result.Succeeded)
                {
                    IdentityResult roleResult;

                    // Assign role
                    if (user.UserType == "Customer")
                    {
                        roleResult = await Manager.AddToRoleAsync(user, "Customer");
                    }
                    else
                    {
                        roleResult = await Manager.AddToRoleAsync(user, "Seller");
                    }

                    if (roleResult.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false);
                        return RedirectToAction("Login");
                    }
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Register", register);
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists
                ApplicationUser user = await Manager.FindByEmailAsync(login.email);
                if (user != null)
                {
                    if (await Manager.CheckPasswordAsync(user, login.Password) == true)
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("Email", user.Email),
                            new Claim("Password", user.PasswordHash)
                        };

                        await SignInManager.SignInWithClaimsAsync(user, login.RememberMe, claims);
                        return RedirectToAction("Screen", "Screen");
                    }
                }
                ModelState.AddModelError("", "The password or username is incorrect.");
            }
            return View("Login", login);
        }
          

        







    }
}
