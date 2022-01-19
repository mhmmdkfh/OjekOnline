using DriverService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverService.Data
{
    public interface IDriver : ICrud<Driver>
    {
        Task<IEnumerable<Driver>> GetByName(string name);
    }
}
