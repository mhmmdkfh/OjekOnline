using DriverService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DriverService.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using DriverService.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace DriverService.Data
{
    public class DriverDAL : IDriver
    {
        private readonly AppDbContext _db;
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DriverDAL(AppDbContext db, IOptions<TokenSettings> tokenSettings, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _tokenSettings = tokenSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Driver> GetById(string id)
        {
            try
            {
                var result = await _db.Drivers.Where(d => d.Id == Convert.ToInt32(id)).SingleOrDefaultAsync<Driver>();
                if (result == null) throw new Exception($"Data id {id} tidak ditemukan !");
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"error: {dbEx.Message}");
            }
        }

        public async Task<DriverTokenDto> Login(string email, string password)
        {
            var driver = await _db.Drivers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (driver == null)
            {
                var msg = new DriverTokenDto
                {
                    Message = "Username or password was invalid"
                };
                return msg;
            }
            bool valid = BCrypt.Net.BCrypt.Verify(password, driver.Password);
            if (valid)
            {
                if (driver.IsActive == false)
                {
                    var result1 = new DriverTokenDto
                    {
                        Message = "The account is not active, please contact the admin center"
                    };
                    return result1;
                }
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, driver.Email));
                claims.Add(new Claim("id", driver.Id.ToString()));

                var expired = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: _tokenSettings.Value.Issuer,
                    audience: _tokenSettings.Value.Audience,
                    expires: expired,
                    claims: claims,
                    signingCredentials: credentials
                );
                var result = new DriverTokenDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Expired = expired.ToString(),
                    Message = "success"
                };
                return result;
            }
            var msg1 = new DriverTokenDto
            {
                Message = "Username or password was invalid"
            };
            return msg1;
        }

        public async Task<Driver> Registration(Driver obj)
        {
            try
            {
                var drivers = await _db.Drivers.Where(d => d.Email == obj.Email).SingleOrDefaultAsync<Driver>();
                if (drivers != null)
                {
                    throw new System.Exception("Driver all ready exist");
                }
                var newDriver = new Driver
                {
                    Username = obj.Username,
                    FullName = obj.FullName,
                    Email = obj.Email,
                    Phone = obj.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(obj.Password),
                    IsActive = false
                };

                _db.Drivers.Add(newDriver);
                await _db.SaveChangesAsync();
                return newDriver;

            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Error: {ex.Message}");
            }
        }
        public async Task<IEnumerable<Driver>> GetAll()
        {
            var results = await (from d in _db.Drivers orderby d.FullName ascending select d).ToListAsync();
            return results;
        }

        public async Task<Driver> SetLocation(Driver obj)
        {

            try
            {
                var driverClaim = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
                var result = await GetById(driverClaim);
                result.Long = obj.Long;
                result.Lat = obj.Lat;
                await _db.SaveChangesAsync();
                obj.Id = Convert.ToInt32(driverClaim);
                return obj;
            }
            catch (Exception dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public Driver ViewProfile()
        {
            var driverEmail = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            return _db.Drivers.FirstOrDefault(d => d.Id == Convert.ToInt32(driverEmail));
        }

        public Driver ViewWallet()
        {
            var driverWallet = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            return _db.Drivers.FirstOrDefault(d => d.Email == driverWallet);
        }

        public async Task<Driver> Approve(LockDriverInput input)
        {
            var driver = await _db.Drivers.Where(d => d.Id == input.Id).FirstOrDefaultAsync();
            if (driver != null)
            {
                driver.IsActive = input.IsActive;
            }
            _db.Drivers.Update(driver);
            await _db.SaveChangesAsync();
            return driver;
        }
        public async Task<Driver> Lock(LockDriverInput input)
        {
            var driver = await _db.Drivers.Where(d => d.Id == input.Id).FirstOrDefaultAsync();
            if (driver != null)
            {
                driver.IsActive = input.IsActive;
            }
            _db.Drivers.Update(driver);
            await _db.SaveChangesAsync();
            return driver;
        }

        public async Task<Driver> UpdateWalletDriver(Driver obj)
        {
            try
            {
                var driverClaim = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
                var result = await GetById(driverClaim);
                result.Username = result.Username;
                result.Email = result.Email;
                result.Saldo = result.Saldo + obj.Saldo;
                await _db.SaveChangesAsync();
                obj.Id = Convert.ToInt32(driverClaim);
                return obj;
            }
            catch (Exception dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
    }
}
