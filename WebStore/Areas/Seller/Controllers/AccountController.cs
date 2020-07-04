using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.Seller.Models;
using WebStore.Areas.Seller.ViewModels;

namespace WebStore.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserSeller> _userManager;
        private readonly SignInManager<UserSeller> _signInManager;
        public AccountController(UserManager<UserSeller> userManager, SignInManager<UserSeller> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserSeller user = new UserSeller()
                {
                    Email = model.Email,
                    UserName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home", new { Area = ""});
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                UserSeller user = await _userManager.FindByEmailAsync(model.Email);
                var result =
                    await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and (or) password!");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
