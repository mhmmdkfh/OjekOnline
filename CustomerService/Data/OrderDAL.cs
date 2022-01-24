using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Dtos;
using CustomerService.Helpers;
using CustomerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomerService.Data
{
    public class OrderDAL : IOrder
    {
        private AppDbContext _db;
        /*private AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;*/

        public OrderDAL(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                var orders = await _db.Orders.Where(o => o.IsFinished == true).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersNotAccept(int Driverid)
        {
            try
            {
                var found = await _db.Orders.Where(o => o.DriverId == Driverid && o.IsAccepted == false).ToListAsync();
                return found;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> GetByCustomer(int CustomerId)
        {
            try
            {
                var found = await _db.Orders.Where(it => it.CustomerId == CustomerId).ToListAsync();
                return found;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

        }

        public async Task<IEnumerable<Order>> GetOrderById(int Driverid)
        {
            try
            {
                var found = await _db.Orders.Where(o => o.DriverId == Driverid).ToListAsync();
                return found;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            
        }

        public async Task<AcceptOrderRespon> IsAcceptOrder(AcceptOrderInput input)
        {
            var order = _db.Orders.Where(c => c.Id == input.Id && c.DriverId == input.DriverId).FirstOrDefault();
            if(order == null)
            {
                var msg = new AcceptOrderRespon
                {
                    Message = "Order data could not be found"
                };
                return msg;
            }
            order.IsAccepted = input.IsAccepted;
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
            var result = new AcceptOrderRespon
            {
                Message = "Driver has accepted the order"
            };
            return result;
        }


        public async Task<FinishOrderRespon> IsFinishOrder(FinishOrderInput input)
        {
            var order = _db.Orders.Where(c => c.Id == input.Id && c.DriverId == input.DriverId).FirstOrDefault();
            if (order == null)
            {
                var msg = new FinishOrderRespon
                {
                    Message = "Order data could not be found"
                };
                return msg;
            }
            order.IsFinished = input.IsFinished;
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();

            var result = new FinishOrderRespon
            {
                Message = "Driver has finished the order"
            };
            return result;
        }
    }
}
