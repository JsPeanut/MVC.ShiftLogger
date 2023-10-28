using Microsoft.AspNetCore.Mvc;
using MVC.ShiftLogger.Models;
using System.Diagnostics;

namespace MVC.ShiftLogger.Controllers
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
            return View();
        }
    }
}