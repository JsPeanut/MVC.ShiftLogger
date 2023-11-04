using MVC.ShiftLogger.Data;

namespace MVC.ShiftLogger.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public ApplicationUser Employee { get; set; }

        public List<Shift> EmployeeShifts { get; set; }
    }
}
