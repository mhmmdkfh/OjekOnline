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
        private AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderDAL(AppDbContext db)
        {
            _db = db;
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
    }
}
