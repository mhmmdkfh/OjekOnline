using AutoMapper;
using DriverService.Data;
using DriverService.Dtos;
using DriverService.Dtos.Order;
using DriverService.Models;
using DriverService.SyncDataServices.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace DriverService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDriverDataClient _driverDataClient;
        private readonly IDriver _driver;
        private readonly IMapper _mapper;

        public OrdersController(HttpClient http, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDriverDataClient driverDataClient, IDriver driver, IMapper mapper)
        {
            _http = http;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _driverDataClient = driverDataClient;
            _driver = driver;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            Console.WriteLine("--> connect service");
            var response = _http.GetAsync(_configuration["CustomerService"]).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Get to CustomerService(GetOrderFinished) Was Ok");
                var customerJsonString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(customerJsonString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return Ok(deserialized);
            }
            else
            {
                Console.WriteLine("--> Sync Get to CustomerService failed");
                return NotFound();
            }
        }

        [HttpGet("GetAcceptOrderById")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAcceptOrderByDriverId()
        {
            var driver = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var response = _http.GetAsync(_configuration["CustomerService"] + $"/{Convert.ToInt32(driver)}").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Get to CustomerService(GetOrderNotAcceptedOrderFalse) Was Ok");
                var customerJsonString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(customerJsonString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return Ok(deserialized);
            }
            else
            {
                Console.WriteLine("--> Sync Get to CustomerService failed");
                return NotFound();
            }
        }

        [HttpGet("GetOrderByDriver")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderById()
        {
            var driver = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var response = _http.GetAsync(_configuration["CustomerService"] + $"/Driverid?Driverid={Convert.ToInt32(driver)}").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Get to CustomerService(GetAllOrderByDriver) Was Ok");
                var customerJsonString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonSerializer.Deserialize<IEnumerable<OrderDto>>(customerJsonString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return Ok(deserialized);
            }
            else
            {
                Console.WriteLine("--> Sync Get to CustomerService failed");
                return NotFound();
            }
        }

        [HttpPut("AcceptOrder")]
        public async Task<ActionResult<AcceptOrderDto>> AcceptOrder(AcceptOrderInput input)
        {
            var driver = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var Accept = new AcceptOrderDto
            {
                Id = input.Id,
                DriverId = Convert.ToInt32(driver),
                IsAccepted = true
            };

            try
            {
                await _driverDataClient.SendAcceptedOrder(Accept);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }
            return Ok(Accept);
        }

        [HttpPut("FinishOrder")]
        public async Task<ActionResult<FinishOrderDto>> FinishOrder(FinishOrderInput input)
        {
            var driver = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var Finish = new FinishOrderDto
            {
                Id = input.Id,
                DriverId = Convert.ToInt32(driver),
                CustomerId = input.CustomerId,
                Saldo = input.Saldo,
                IsFinished = input.IsFinished
            };
            try
            {
                await _driverDataClient.SendFinishOrder(Finish);
                var wallet = _mapper.Map<Driver>(input);
                var result = await _driver.UpdateWalletDriver(wallet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }
            return Ok(Finish);
        }
    }
}
