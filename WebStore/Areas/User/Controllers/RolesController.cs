using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.User.Models;

namespace WebStore.Areas.User.Controllers
{
    [Area("User")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserProfile> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<UserProfile> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_roleManager.Roles.ToList());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { Area = "" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if(role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
