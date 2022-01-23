using AdminService.Data.Database;
using AdminService.Data.Dto;
using AdminService.Data.Dto.Input;
using AdminService.Data.Interface;
using AdminService.Models;
using GeoCoordinatePortable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Data
{
    public class AdminRepo : IAdmin
    {
        private AppDbContext _db;
        private IOptions<TokenSettings> _token;

        public AdminRepo(AppDbContext db, IOptions<TokenSettings> token)
        {
            _db = db;
            _token = token;
        }

        public async Task<Admin> RegisterAdmin(CreateAdmin create)
        {
            try
            {
                var valid = await _db.Admins.Where(e=> e.Email == create.Email).FirstOrDefaultAsync();
                if (valid != null)
                {
                    throw new Exception("Email sudah terdaftar");
                }
                var newAdmin = new Admin
                {
                    FullName = create.FullName,
                    Email = create.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(create.Password)
                };
                _db.Admins.Add(newAdmin);
                await _db.SaveChangesAsync();
                return newAdmin;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<TokenUser> Login(LoginInput input)
        {
            var user = await _db.Admins.Where(x => x.Email == input.Email).FirstOrDefaultAsync();
            var valid = BCrypt.Net.BCrypt.Verify(input.Password, user.Password);
            if (user == null && !valid)
            {
                throw new Exception("Email atau password salah");
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("role", "admin"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expired = DateTime.Now.AddHours(3);
            var token = new JwtSecurityToken(
                issuer: _token.Value.Issuer,
                audience: _token.Value.Audience,
                expires: expired,
                signingCredentials: credentials
            );
            return await Task.FromResult(new TokenUser
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }

        public async Task<Rate> SetPrice(float inputPrice)
        {
            var price = await _db.Rates.OrderBy(e => e.TravelFares).FirstOrDefaultAsync();
            price.TravelFares = inputPrice;
            _db.Rates.Update(price);
            await _db.SaveChangesAsync();
            return price;
        }

        public async Task<Rate> GetPrice()
        {
            var rate = await _db.Rates.OrderBy(e => e.TravelFares).FirstOrDefaultAsync();
            return rate;
        }

    }
}
