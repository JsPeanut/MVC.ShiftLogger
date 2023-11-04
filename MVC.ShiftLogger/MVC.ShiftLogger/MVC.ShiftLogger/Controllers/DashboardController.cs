using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.ShiftLogger.Data;
using MVC.ShiftLogger.Models.ViewModel;

namespace MVC.ShiftLogger.Controllers
{
    [Authorize(Roles = "Manager")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _identityContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(ApplicationDbContext identityContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _identityContext = identityContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var employees = _userManager.GetUsersInRoleAsync("Member");

            var dashboardViewModel = new DashboardViewModel
            {
                Employees = employees.Result.ToList()
            };

            return View(dashboardViewModel);
        }
    }
}
