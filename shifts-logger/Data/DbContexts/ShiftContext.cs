using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using shifts_logger.Models;

namespace shifts_logger.Data.DbContexts
{
        public class YourDbContext : DbContext
        {
            public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

            public DbSet<Shift> Shifts { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder builder)
            {
                builder.UseSqlServer();
            }
        }
}
