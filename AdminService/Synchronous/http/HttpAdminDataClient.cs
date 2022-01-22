using CustomerService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetDataUsersToAdmin()
        {
            var response = _http.GetAsync(_config["CustomerService"]).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CustomerService was OK !");
                var customerJsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + customerJsonString);
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<Customer>>(custome‌​rJsonString);
                return deserialized;
            }
            else
            {
                Console.WriteLine("--> Sync POST to CustomerService failed");
                return null;
            }
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
