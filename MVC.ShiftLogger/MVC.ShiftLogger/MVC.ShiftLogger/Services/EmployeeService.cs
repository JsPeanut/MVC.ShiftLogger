using Microsoft.AspNetCore.Identity;
using MVC.ShiftLogger.Data;

namespace MVC.ShiftLogger.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmployeeService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void DeleteEmployee(string id) 
        {
            var employee = _userManager.FindByIdAsync(id).Result;

            var result = _userManager.DeleteAsync(employee);

            _context.SaveChangesAsync();
        }
    }
}
