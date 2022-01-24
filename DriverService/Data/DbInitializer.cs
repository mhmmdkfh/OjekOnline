using DriverService.Models;
using System;
using System.Linq;

namespace DriverService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (context.Drivers.Any())
            {
                return;
            }

            /*Console.WriteLine("--> Seeding data...");*/
            var drivers = new Driver[]
            {
                new Driver{FullName = "Driver 1",Username="driver1" ,Email = "driver1@gmail.com", Saldo = 30000, Phone = "812332", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsActive = true},
                new Driver{FullName = "Driver 2",Username="driver2" ,Email = "driver2@gmail.com", Saldo = 51000, Phone = "81239", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsActive = true}
            };

            foreach(var driver in drivers)
            {
                context.Drivers.Add(driver);
            }

            context.SaveChanges();
        }
    }
}
