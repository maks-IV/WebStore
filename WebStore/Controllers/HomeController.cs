using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductContext db;

        public HomeController(ILogger<HomeController> logger, ProductContext productContext)
        {
            _logger = logger;
            db = productContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.Products = await db.Products.Include(c => c.Category).ToListAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
