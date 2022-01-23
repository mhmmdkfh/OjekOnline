using AdminService.Data.Dto.Input;
using System.Threading.Tasks;

namespace AdminService.Synchronous.http
{
    public interface IAdminDataClient
    {
        Task SendApprove(LockDriverInput input);
        Task SendLockUser(LockInput input);
        Task SendLockDriver(LockDriverInput input);
    }
}
