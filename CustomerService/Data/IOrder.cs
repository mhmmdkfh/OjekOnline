using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Dtos;
using CustomerService.Models;
namespace CustomerService.Data
{
    public interface IOrder
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersNotAccept(int Driverid);
        Task<IEnumerable<Order>> GetOrderById(int Driverid);
        Task<IEnumerable<Order>> GetByCustomer(int CustomerId);
        Task<AcceptOrderRespon> IsAcceptOrder(AcceptOrderInput input);
        Task<FinishOrderRespon> IsFinishOrder(FinishOrderInput input);
    }
}
