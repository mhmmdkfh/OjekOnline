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
        Task<IEnumerable<Driver>> GetAll();
        Driver ViewProfile();
        Driver ViewWallet();
    }
}
