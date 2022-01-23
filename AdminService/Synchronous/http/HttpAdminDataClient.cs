using AdminService.Dtos;
using AdminService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public Task LockDriver()
        {
            throw new System.NotImplementedException();
        }

        public async Task SendLockUser(LockInput input)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(input),
                Encoding.UTF8, "application/json");
            var response = await _http.PutAsync(_config["CustomerService"], httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PUT to CustomerService was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync PUT to CustomerService failed");
            }
        }
    }
}
