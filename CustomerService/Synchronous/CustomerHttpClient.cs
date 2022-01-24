using CustomerService.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CustomerService.Synchronous
{
    public class CustomerHttpClient
    {
        public static async Task<string> GetPrice(AppSettings _appSettings, string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Split(' ')[1]);
                var response = await client.GetAsync(_appSettings.AdminService + "/api/Admin");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Console.WriteLine(responseBody.ToString());
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("--> Sync PUT to CustomerService was OK !");
                }
                else
                {
                    Console.WriteLine("--> Sync PUT to CustomerService failed");
                };
                return responseBody.ToString();
            }

        }
    }
}