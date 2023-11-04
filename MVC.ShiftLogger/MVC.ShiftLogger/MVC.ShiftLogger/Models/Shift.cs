namespace MVC.ShiftLogger.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan WorkedTime { get; set; }
    }
}
