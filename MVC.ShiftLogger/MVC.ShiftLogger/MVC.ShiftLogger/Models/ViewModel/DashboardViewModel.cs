using MVC.ShiftLogger.Data;

namespace MVC.ShiftLogger.Models.ViewModel
{
    public class DashboardViewModel
    {
        public List<Shift> Shifts { get; set; }
        public List<ApplicationUser> Employees { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
