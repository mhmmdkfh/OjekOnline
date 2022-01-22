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
                new Driver{NIK = "3123123",FullName = "Driver 1",Email = "driver1@gmail.com", Wallet = 30000, Phone = "812332", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsAccepted = true},
                new Driver{NIK = "1231231",FullName = "Driver 2",Email = "driver2@gmail.com", Wallet = 51000, Phone = "81239", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsAccepted = true}
            };

            foreach(var driver in drivers)
            {
                context.Drivers.Add(driver);
            }

            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{NIK = "4123123",FullName = "Customer 1",Email = "customer1@gmail.com", Wallet = 5000, Phone = "2812332", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsAccepted = true},
                new Customer{NIK = "4231231",FullName = "Customer 2",Email = "customer2@gmail.com", Wallet = 51000, Phone = "281239", Password = BCrypt.Net.BCrypt.HashPassword("Kosongkan@Saja"), IsAccepted = true}
            };

            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();

            var orders = new Order[]
            {
                new Order{DriverId = 1, CustomerId = 1, Lat = 1234, Long = 1421.123, Price = 15000, IsAccepted = true , IsFinished = true},
                new Order{DriverId = 2, CustomerId = 1, Lat = 1234, Long = 1423.133, Price = 15000, IsAccepted = true , IsFinished = true}
            };

            foreach (var order in orders)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();

            var rates = new Rate[]
            {
                new Rate{ TravelFares = 10000}
            };

            foreach (var rate in rates)
            {
                context.Rates.Add(rate);
            };
        }
    }
}
