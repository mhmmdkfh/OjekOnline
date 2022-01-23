﻿using AdminService.Data.Dto;
using AdminService.Data.Dto.Input;
using AdminService.Data.Interface;
using AdminService.Dtos;
using AdminService.Models;
using AdminService.Synchronous.http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
        private IAdminDataClient _httpAdmin;

        public AdminController(IAdmin admin,
            IMapper mapper, HttpClient http, IConfiguration configuration, IAdminDataClient adminDataClient)
        {
            _admin = admin;
            _mapper = mapper;
            _http = http;
            _config = configuration;
            _httpAdmin = adminDataClient;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] CreateAdmin create)
        {
            try
            {
                var data = await _admin.RegisterAdmin(create);
                return Ok(new { Message = "Register berhasil" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenSettings>> Login([FromBody] LoginInput input)
        {
            try
            {
                var user = await _admin.Login(input);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
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
                var deserialized = JsonSerializer.Deserialize<IEnumerable<Driver>>(driverJsonString,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

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
      /*  [HttpPut("Drivers/{id}")]
        public async Task<ActionResult<Driver>> ApproveDriver(int id)
        {

        }

        // PUT api/<AdminController>/lock/Drivers/5
        [HttpPut("lock/Drivers/{Id}")]
        public async Task<ActionResult<Driver>> LockDriver(int id, [FromBody] bool input)
        {

        } */

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
                var deserialized = JsonSerializer.Deserialize<IEnumerable<Customer>>(customerJsonString,
                   new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

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
        [HttpPut("lock/Users")]
        public async Task<ActionResult> LockUser([FromBody]LockInput input)
        {
            try
            {
                await _httpAdmin.SendLockUser(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AdminController>/Orders
      /*  [HttpGet("Orders")]
        public async Task<ActionResult<IEnumerable<OrderData>>> GetAllTransactions()
        {

        } */

        // PUT api/<AdminController>
        [HttpPut]
        public async Task<Rate> GetPrice()
        {
            var setPrice = await _admin.GetPrice();
            return setPrice;
        }
    }
}
