using AdminService.Dtos;
using System.Threading.Tasks;

namespace AdminService.Synchronous.http
{
    public interface IAdminDataClient
    {
        Task SendLockUser(LockInput input);
        Task LockDriver();
    }
}
