using AdminService.Data.Dto;
using AdminService.Data.Dto.Input;
using AdminService.Data.Interface;
using AdminService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public AdminController(IAdmin admin, IMapper mapper)
        {
            _admin = admin;
            _mapper = mapper;
        }

        // GET: api/<AdminController>/Drivers
        [HttpGet("Drivers")]
        public async Task<ActionResult<IEnumerable<DriverData>>> GetAllDrivers()
        {
            var data = await _admin.GetDrivers();
            var drivers = _mapper.Map<DriverData>(data);
            return Ok(drivers);
        }


        // PUT api/<AdminController>
        [HttpPut("Drivers/{id}")]
        public async Task<ActionResult<Driver>> ApproveDriver(int id, [FromBody] bool input)
        {
            try
            {
                var data = await _admin.Approve(id);
                return Ok(data);
            }
            catch (System.Exception ex)
            {

                return BadRequest(new { ex.Message });
            }
        }

        // PUT api/<AdminController>/lock/Drivers/5
        [HttpPut("lock/Drivers/{Id}")]
        public async Task<ActionResult<Driver>> LockDriver(int id, [FromBody] bool input)
        {
            try
            {
                var data = await _admin.LockDriver(id, input);
                return Ok(data);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        // GET api/<AdminController>/Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<CustomerData>>> GetAllUsers()
        {
            var data = await _admin.GetUsers();
            var users = _mapper.Map<CustomerData>(data);
            return Ok(users);
        }

        // PUT api/<AdminController>/Users/3
        [HttpPut("Users/{id}")]
        public async Task<ActionResult<Customer>> LockUser(int id, [FromBody] bool input)
        {
            try
            {
                var data = await _admin.LockUser(id, input);
                return Ok(data);

            }
            catch (System.Exception ex)
            {

                return BadRequest(new { ex.Message });
            }
        }

        // GET api/<AdminController>/Orders
        [HttpGet("Orders")]
        public async Task<ActionResult<IEnumerable<OrderData>>> GetAllTransactions()
        {
            var data = await _admin.GetAllTransaction();
            var result = _mapper.Map<OrderData>(data);
            return Ok(result);
        }

        // PUT api/<AdminController>
        [HttpPut]
        public async Task<Order> GetPrice(DriverInput driver)
        {
            try
            {
                var setPrice = await _admin.GetPrice(driver);
                return setPrice;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
