using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverService.Data
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task<T> Update(string id, T obj);
        Task Delete(string id);
    }
}
