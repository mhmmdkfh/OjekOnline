using AdminService.Data.Database;
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
            return driver;
            
        }   
        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            var drivers = await _db.Drivers.OrderBy(d => d.Id).ToListAsync();
            return drivers;
        }

        public async Task<Driver> LockDriver(int id)
        {
            var driver = await _db.Drivers.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (driver == null)
            {
                throw new Exception($"Driver with no id {id} not found ");
            }
            driver.IsAccepted = false;
            return driver;
        }

        // User
        public async Task<IEnumerable<Customer>> GetUsers()
        {
            var customer = await _db.Customers.OrderBy(u => u.Id).ToListAsync();
            return customer;
        }

        public async Task<Customer> LockUser(int id)
        {
            var user = await _db.Customers.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception($"User with no id {user.Id} not found ");
            }
            user.IsAccepted = false;
            return user;
        }

        // Order
        public async Task<IEnumerable<Order>> GetAllTransaction()
        {
            var transactions = await _db.Orders.Where(t => t.IsAccepted == true && t.IsFinished == true).ToListAsync();
            return transactions;
        }

        public async Task<Order> SetPrice(Driver driver)
        {
            var order = await _db.Orders.Where(o => o.Driver.Id == driver.Id && o.IsAccepted == true).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new Exception("Order belum diterima driver");
            }
            var firstLoc = new GeoCoordinate(order.Lat, order.Long);
            var scdLoc = new GeoCoordinate(driver.Lat, driver.Long);

            var distance = firstLoc.GetDistanceTo(scdLoc) / 1000;

            order.Price = Convert.ToInt32(distance * 10000);
            return order;
        }
    }
}
