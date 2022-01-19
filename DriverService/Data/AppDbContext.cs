using DriverService.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
