using AdminService.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminService.Data.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
