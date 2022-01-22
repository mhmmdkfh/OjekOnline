using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Models;
namespace CustomerService.Data
{
    public interface IOrder
    {
        Task<IEnumerable<Order>> GetByCustomer(int CustomerId);
    }
}
