using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.Manage.ViewModels;
using WebStore.Areas.User.Models;

namespace WebStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProfileController : Controller
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly SignInManager<UserProfile> _signInManager;

        public ProfileController(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                IndexViewModel model = new IndexViewModel
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber
                };
                return View(model);
            }
            else
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            model.UserName = user.UserName;

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if(model.PhoneNumber != phoneNumber)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if(!result.Succeeded)
                {
                    model.StatusMessage = "Unexpected error when trying to set phone number.";
                    return View(model);
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            model.StatusMessage = "Your profile has been updated!";
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if(user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                await _signInManager.RefreshSignInAsync(user);
                model.StatusMessage = "Your password has been changed.";
                return View(model);
            }
            return View(model);
        }
    }
}
