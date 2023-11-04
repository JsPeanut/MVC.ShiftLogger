using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.ShiftLogger.Data;
using MVC.ShiftLogger.Models.ViewModel;
using MVC.ShiftLogger.Services;

namespace MVC.ShiftLogger.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeController : Controller
    {
        private readonly ShiftService _shiftService;
        private readonly EmployeeService _employeeService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmployeeController(ShiftService shiftService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, EmployeeService employeeService)
        {
            _shiftService = shiftService;
            _roleManager = roleManager;
            _userManager = userManager;
            _employeeService = employeeService;
        }
        public IActionResult Index(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var evm = new EmployeeViewModel
            {
                Employee = user,
                EmployeeShifts = _shiftService.ReadShifts().Where(s => s.FirstName == user.FirstName && s.LastName == user.LastName).ToList()
            };
            return View(evm);
        }

        public IActionResult DeleteEmployee(string id) 
        {
            _employeeService.DeleteEmployee(id);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
