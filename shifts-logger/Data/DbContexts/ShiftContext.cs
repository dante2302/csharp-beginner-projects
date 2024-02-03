using Microsoft.EntityFrameworkCore;
using shifts_logger.Models;

namespace shifts_logger.Data.DbContexts
{
    public class ShiftContext : DbContext
    {
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Configuration.Manager.GetConnectionString("mydb"));
        }
    }
}
