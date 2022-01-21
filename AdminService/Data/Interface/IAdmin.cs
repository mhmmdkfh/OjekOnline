using AdminService.Data.Dto.Input;
using AdminService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminService.Data.Interface
{
    public interface IAdmin
    {
        // Driver
        Task<IEnumerable<Driver>> GetDrivers();
        Task<Driver> Approve(int id);
        Task<Driver> LockDriver(int id, bool input);

        // User
        Task<IEnumerable<Customer>> GetUsers();
        Task<Customer> LockUser(int id, bool input);

        //Order
        Task<Rate> SetPrice(int inputPrice);
        Task<IEnumerable<Order>> GetAllTransaction();
        Task<Order> GetPrice(DriverInput driver);
    }
}
