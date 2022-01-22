using AdminService.Data.Dto;
using AdminService.Data.Dto.Input;
using AdminService.Data.Interface;
using AdminService.Models;
using AdminService.Synchronous.http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdmin _admin;
        private IMapper _mapper;
        private HttpClient _http;
        private IConfiguration _config;

        public AdminController(IAdmin admin,
            IMapper mapper, HttpClient http, IConfiguration configuration)
        {
            _admin = admin;
            _mapper = mapper;
            _http = http;
            _config = configuration;
        }

        // GET: api/<AdminController>/Drivers
        [HttpGet("Drivers")]
        public async Task<ActionResult<IEnumerable<DriverData>>> GetAllDrivers()
        {
            var response = _http.GetAsync(_config["DriverService"]).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Get to CustomerService was OK !");
                var driverJsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + driverJsonString);
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<Driver>>(driverJsonString);

                var data = _mapper.Map<CustomerData>(deserialized);
                return Ok(data);
            }
            else
            {
                Console.WriteLine("--> Sync POST to CustomerService failed");
                return NotFound();
            }
        }


        // PUT api/<AdminController>
        [HttpPut("Drivers/{id}")]
        public async Task<ActionResult<Driver>> ApproveDriver(int id)
        {

        }

        // PUT api/<AdminController>/lock/Drivers/5
        [HttpPut("lock/Drivers/{Id}")]
        public async Task<ActionResult<Driver>> LockDriver(int id, [FromBody] bool input)
        {

        }

        // GET api/<AdminController>/Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<CustomerData>>> GetAllUsers()
        {
            var response = _http.GetAsync(_config["CustomerService"]).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Get to CustomerService was OK !");
                var customerJsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Your response data is: " + customerJsonString);
                var deserialized = JsonConvert.DeserializeObject<IEnumerable<Customer>>(custome‌​rJsonString);

                var data = _mapper.Map<CustomerData>(deserialized);
                return Ok(data);
            }
            else
            {
                Console.WriteLine("--> Sync POST to CustomerService failed");
                return NotFound();
            }
        }

        // PUT api/<AdminController>/Users/3
        [HttpPut("Users/{id}")]
        public async Task<ActionResult<Customer>> LockUser(int id, [FromBody] bool input)
        {

        }

        // GET api/<AdminController>/Orders
        [HttpGet("Orders")]
        public async Task<ActionResult<IEnumerable<OrderData>>> GetAllTransactions()
        {

        }

        // PUT api/<AdminController>
        [HttpPut]
        public async Task<Rate> GetPrice()
        {
            var setPrice = await _admin.GetPrice();
            return setPrice;
        }
    }
}
