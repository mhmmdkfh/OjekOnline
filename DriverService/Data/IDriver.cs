using DriverService.Dtos;
using DriverService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverService.Data
{
    public interface IDriver
    {
        Task<Driver> Registration(Driver obj);
        Task<Driver> Authenticate(string email, string password);
    }
}
