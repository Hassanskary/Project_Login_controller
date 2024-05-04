using JopPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JopPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            ViewBag.Email = HttpContext.Session.GetString("Email");

            return View();
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

        public IActionResult Print(string userType) // Accept userType as a parameter
        {
            ViewBag.UserType = userType; // Pass the value to the view

            return View();
        }

    }
}
