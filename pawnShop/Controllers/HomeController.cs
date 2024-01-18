using Microsoft.AspNetCore.Mvc;
using pawnShop.Models;
using pawnShop.Validated;
using System.Diagnostics;

namespace pawnShop.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = new UserModel
            {
                Name = HttpContext.Session.GetString("userName"),
                Role = HttpContext.Session.GetString("userRole")
            };
            return View(viewModel);
        }

 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
