using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.ShiftLogger.Data;
using MVC.ShiftLogger.Models;
using MVC.ShiftLogger.Models.ViewModel;
using MVC.ShiftLogger.Services;

namespace MVC.ShiftLogger.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly ShiftService _shiftService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShiftsController(ShiftService shiftService, UserManager<ApplicationUser> userManager) 
        {
            _shiftService = shiftService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            //User is current logged user
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByNameAsync(userId).Result;
            var shiftList = _shiftService.ReadShifts().Where(s => s.FirstName == user.FirstName && s.LastName == user.LastName).ToList();

            var shiftViewModel = new ShiftViewModel
            {
                Shifts = shiftList
            };

            return View(shiftViewModel);
        }

        public IActionResult AddShift(Shift shift)
        {
            _shiftService.AddShift(shift);

            return Redirect("https://localhost:7230/Shifts");
        }
    }
}
