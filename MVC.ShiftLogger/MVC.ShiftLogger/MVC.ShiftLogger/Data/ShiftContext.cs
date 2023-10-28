using Microsoft.EntityFrameworkCore;
using MVC.ShiftLogger.Models;

namespace MVC.ShiftLogger.Data
{
    public class ShiftContext : DbContext
    {
        public ShiftContext(DbContextOptions<ShiftContext> options)
            : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; } = null!;
    }
}
