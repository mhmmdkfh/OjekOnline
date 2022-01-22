using System.Threading.Tasks;

namespace AdminService.Synchronous.http
{
    public interface IAdminDataClient
    {
        Task GetDataUsersToAdmin();
        Task GetDataDriversToAdmin();

        Task LockUser();
        Task LockDriver();
    }
}
