using DriverService.Dtos.Order;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DriverService.SyncDataServices.Http
{
    public class HttpDriverDataClient : IDriverDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpDriverDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendAcceptedOrder(AcceptOrderDto input)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(input),
              Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_configuration["CustomerService"] + "/AcceptOrder",  httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PUT to CustomerService was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync PUT to CustomerService failed");
            };
        }

        public async Task SendFinishOrder(FinishOrderInput input)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(input),
              Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_configuration["CustomerService"] + "/FinishOrder", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PUT to CustomerService was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync PUT to CustomerService failed");
            };
        }
    }
}
