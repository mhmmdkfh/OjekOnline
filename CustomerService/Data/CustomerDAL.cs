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
using CustomerService.Synchronous;
using Geolocation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomerService.Data
{
    public class CustomerDAL : ICustomer
    {
        private AppDbContext _db;
        private AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IOrder _order;

        public CustomerDAL(AppDbContext db,
        IOptions<AppSettings> appSettings,
        IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _order = new OrderDAL(db);
        }

        public async Task<Customer> Insert(Customer obj)
        {
            try
            {
                _db.Customers.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

        }
        public async Task Register(RegisterInput user)
        {
            try
            {
                var newCustomer = new Customer
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Saldo = 0,
                    IsActive = true,
                };
                var result = await Insert(newCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public async Task<LoginResponse> Login(LoginInput customer)
        {
            var found = await _db.Customers.Where(x => x.Username == customer.Username).FirstOrDefaultAsync();
            if (found == null)
            {
                var msg = new LoginResponse
                {
                    Token = null,
                    Message = "Username or password was invalid"
                };
                return msg;
            }
            bool valid = BCrypt.Net.BCrypt.Verify(customer.Password, found.Password);
            if (valid == true)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>();
                claims.Add(new Claim("id", found.Id.ToString()));
                claims.Add(new Claim("username", found.Username));
                claims.Add(new Claim("email", found.Email));
                claims.Add(new Claim("role", "customer"));
                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    expires: expired,
                    claims: claims,
                    signingCredentials: credentials
                );
                var result = new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Message = null
                };
                return result;
            }
            var msg1 = new LoginResponse
            {
                Token = null,
                Message = "Username or password was invalid"
            };
            return msg1;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await _db.Customers.OrderBy(e => e.Username).ToListAsync();
            return customers;
        }
        public async Task<CheckOrderFeeResponse> CheckOrderFee(CheckOrderFeeRequest request)
        {
            var rateBody = await CustomerHttpClient.GetPrice(_appSettings, _httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            // var rate = 10000;
            dynamic data = Newtonsoft.Json.Linq.JObject.Parse(rateBody);
            var rate = (double)data.travelFares;
            Console.WriteLine(rate);
            double distance = GeoCalculator.GetDistance(request.FromLat, request.FromLong, request.ToLat, request.ToLong, 1);
            return new CheckOrderFeeResponse
            {
                FromLat = request.FromLat,
                FromLong = request.FromLong,
                ToLat = request.ToLat,
                ToLong = request.ToLong,
                Distance = distance,
                Rate = rate,
                Price = distance * rate
            };
        }
        public async Task<TopUpResponse> TopUp(TopUpRequest request)
        {
            var customerId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            Console.WriteLine(customerId);
            var found = await _db.Customers.Where(it => it.Id == int.Parse(customerId)).FirstOrDefaultAsync();
            found.Saldo = found.Saldo + request.Total;
            await _db.SaveChangesAsync();
            var response = new TopUpResponse
            {
                Username = found.Username,
                Saldo = found.Saldo
            };
            return response;
        }
        public async Task<ViewSaldoResponse> ViewSaldo()
        {
            var customerId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            Console.WriteLine(customerId);
            var found = await _db.Customers.Where(it => it.Id == int.Parse(customerId)).FirstOrDefaultAsync();
            await _db.SaveChangesAsync();
            var response = new ViewSaldoResponse
            {
                Username = found.Username,
                Saldo = found.Saldo
            };
            return response;
        }
        public async Task<IEnumerable<Order>> ViewOrderHistory()
        {
            var CustomerId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            Console.WriteLine(CustomerId);
            var response = await _order.GetByCustomer(int.Parse(CustomerId));
            return response;
        }

        public async Task<Customer> LockUser(LockInput input)
        {
            var customer = _db.Customers.Where(c => c.Id == input.Id).FirstOrDefault();
            if (customer != null)
            {
                customer.IsActive = input.IsActive;
            }
            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetById(string id)
        {
            try
            {
                var result = await _db.Customers.Where(d => d.Id == Convert.ToInt32(id)).SingleOrDefaultAsync<Customer>();
                if (result == null) throw new Exception($"Data id {id} tidak ditemukan !");
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<Customer> UpdateWalletCustomer(string CustomerId, Customer obj)
        {
            try
            {
                var result = await GetById(CustomerId);
                result.Username = result.Username;
                result.Email = result.Email;
                result.Saldo = result.Saldo - obj.Saldo;
                await _db.SaveChangesAsync();
                obj.Id = Convert.ToInt32(CustomerId);
                return obj;
            }
            catch (Exception dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
    }
}
