using Microsoft.AspNetCore.Mvc;

namespace MVC.ShiftLogger.Controllers
{
    public class ShiftsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
