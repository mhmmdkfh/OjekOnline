using DriverService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DriverService.Data
{
    public class PrepDb
    {
        public static void PrePopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            };
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.Write("--> Menjalankan migrasi");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Gagal Menjalankan Migrasi dengan error: {ex.Message}");
                }
            }

            if (!context.Drivers.Any())
            {
                Console.WriteLine("--> Seeding data Driver...");
                context.Drivers.AddRange(
                    new Driver { FullName = "Driver 1", Username = "driver1", Email = "driver1@gmail.com", Saldo = 30000, Phone = "812332", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsActive = true },
                    new Driver { FullName = "Driver 2", Username = "driver2", Email = "driver2@gmail.com", Saldo = 51000, Phone = "81239", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsActive = true }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have Driver data...");
            }
        }
    }
}
