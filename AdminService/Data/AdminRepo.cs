using AdminService.Data.Database;
using AdminService.Data.Dto.Input;
using AdminService.Data.Interface;
using AdminService.Models;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Data
{
    public class AdminRepo : IAdmin
    {
        private AppDbContext _db;

        public AdminRepo(AppDbContext db)
        {
            _db = db;
        }


        // Driver
        public async Task<Driver> Approve(int id)
        {
            var driver = await _db.Drivers.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (driver == null)
            {
                throw new Exception($"Driver with no id {id} not found ");
            }
            driver.IsAccepted = true;
            _db.Drivers.Update(driver);
            await _db.SaveChangesAsync();
            return driver;
        }   
        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            var drivers = await _db.Drivers.OrderBy(d => d.Id).ToListAsync();
            return drivers;
        }

        public async Task<Driver> LockDriver(int id, bool input)
        {
            var driver = await _db.Drivers.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (driver == null)
            {
                throw new Exception($"Driver with no id {id} not found ");
            }
            driver.IsAccepted = input;
            _db.Drivers.Update(driver);
            await _db.SaveChangesAsync();
            return driver;
        }

        // User
        public async Task<IEnumerable<Customer>> GetUsers()
        {
            var customer = await _db.Customers.OrderBy(u => u.Id).ToListAsync();
            return customer;
        }

        public async Task<Customer> LockUser(int id, bool input)
        {
            var user = await _db.Customers.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception($"User with no id {user.Id} not found ");
            }
            user.IsAccepted = input;
            _db.Customers.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        // Order
        public async Task<IEnumerable<Order>> GetAllTransaction()
        {
            var transactions = await _db.Orders.Where(t => t.IsAccepted == true && t.IsFinished == true).ToListAsync();
            return transactions;
        }

        public async Task<Rate> SetPrice(int inputPrice)
        {
            var price = await _db.Rates.OrderBy(e => e.TravelFares).FirstOrDefaultAsync();
            price.TravelFares = inputPrice;
            _db.Rates.Update(price);
            await _db.SaveChangesAsync();
            return price;
        }

        public async Task<Order> GetPrice(DriverInput driver)
        {
            var order = await _db.Orders.Where(o => o.Driver.Id == driver.Id && o.IsAccepted == true).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new Exception("Order belum diterima driver");
            }

            var firstLoc = new GeoCoordinate(order.Lat, order.Long);
            var scdLoc = new GeoCoordinate(driver.Lat, driver.Long);


            var distance = firstLoc.GetDistanceTo(scdLoc) / 1000;

            var price = _db.Rates.OrderBy(e => e.TravelFares).FirstOrDefault();
            order.Price = Convert.ToInt32(distance * Convert.ToDouble(price));
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
            return order;
        }
    }
}
