using DriverService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DriverService.Dtos;

namespace DriverService.Data
{
    public class DriverDAL : IDriver
    {
        private readonly AppDbContext _db;
        public DriverDAL(AppDbContext db)
        {
            _db = db;
        }

        public Task<Driver> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Driver> Registration(Driver obj)
        {
            try
            {
                var drivers = await _db.Drivers.Where(d => d.NIK == obj.NIK).SingleOrDefaultAsync<Driver>();
                if (drivers != null)
                {
                    throw new System.Exception("Driver all ready exist");
                }
                var newDriver = new Driver
                {
                    NIK = obj.NIK,
                    FullName = obj.FullName,
                    Email = obj.Email,
                    Phone = obj.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(obj.Password),
                    IsAccepted = false
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


        /*public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result == null) throw new Exception("Data tidak ditemukan !");
            try
            {
                _db.Drivers.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            var results = await (from d in _db.Drivers orderby d.FullName ascending select d).ToListAsync();
            return results;
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

        public Task<IEnumerable<Driver>> GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Driver> Update(string id, Driver obj)
        {
            try
            {
                var result = await GetById(id);
                result.NIK = obj.NIK;
                result.FullName = obj.FullName;
                result.Email = obj.Email;
                result.Wallet = obj.Wallet;
                result.Phone = obj.Phone;
                await _db.SaveChangesAsync();
                obj.Id = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }*/
    }
}
