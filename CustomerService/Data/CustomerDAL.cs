using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Dtos;
using CustomerService.Helpers;
using CustomerService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomerService.Data
{
    public class CustomerDAL : ICustomer
    {
        private AppDbContext _db;

        public CustomerDAL(AppDbContext db)
        {
            _db = db;
        }
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppSettings _appSettings;

        public CustomerDAL(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        public async Task<Customer> Login(string username, string password)
        {
            var userFind = await _userManager.CheckPasswordAsync(
                await _userManager.FindByNameAsync(username), password);
            if (!userFind)
                return null;
            var user = new Customer
            {
                Username = username
            };

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            // var roles = await GetRolesFromUser(username);
            // foreach (var role in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, role));
            // }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            // user.Token = tokenHandler.WriteToken(token);
            return user;
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
                    Password = user.Password,
                    Saldo = 0
                };
                var result = await Insert(newCustomer);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
