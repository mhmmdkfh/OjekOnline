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

        public CustomerDAL(AppDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;

            _appSettings = appSettings.Value;
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
                    Saldo = 0
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
            Console.WriteLine(valid);
            Console.WriteLine(_appSettings.Secret);
            if (valid == true)
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>();
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
    }
}
