using DriverService.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
    }
}
