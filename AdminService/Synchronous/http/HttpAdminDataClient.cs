using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminService.Synchronous.http
{
    public class HttpAdminDataClient : IAdminDataClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public HttpAdminDataClient(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _config = configuration;
        }
        public async Task GetDataDriversToAdmin()
        {
            var response = await _http.GetAsync();
        }

        public Task GetDataUsersToAdmin()
        {
            throw new System.NotImplementedException();
        }

        public Task LockDriver()
        {
            throw new System.NotImplementedException();
        }

        public Task LockUser()
        {
            throw new System.NotImplementedException();
        }
    }
}
