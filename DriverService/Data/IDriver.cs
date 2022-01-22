using DriverService.Dtos;
using DriverService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverService.Data
{
    public interface IDriver
    {
        Task<Driver> Registration(Driver obj);
        Task<DriverTokenDto> Login(string email, string password);
        Driver ViewProfile();
        Driver ViewWallet();
    }
}
