using DriverService.Dtos.Order;
using System.Threading.Tasks;

namespace DriverService.SyncDataServices.Http
{
    public interface IDriverDataClient
    {
        Task SendAcceptedOrder(AcceptOrderDto input);
        Task SendFinishOrder(FinishOrderDto input);
    }
}
