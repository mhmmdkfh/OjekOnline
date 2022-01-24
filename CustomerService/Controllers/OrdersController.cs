using AutoMapper;
using CustomerService.Data;
using CustomerService.Dtos;
using CustomerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Controllers
{
    [Route("api/customer/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrder _order;
        private readonly IMapper _mapper;
        private readonly ICustomer _customer;

        public OrdersController(IOrder order, IMapper mapper, ICustomer customer)
        {
            _order = order;
            _mapper = mapper;
            _customer = customer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _order.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpGet("{Driverid}")]
        public async Task<ActionResult<Order>> GetOrdersNotAccept(int Driverid)
        {
            try
            {
                Console.WriteLine("Get Message From DriverService");
                var orders = await _order.GetOrdersNotAccept(Driverid);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("Driverid")]
        public async Task<ActionResult<PriceOrderDto>> GetOrderByDriver(int Driverid)
        {
            try
            {
                Console.WriteLine("Get Message From DriverService");
                var orders = await _order.GetOrderById(Driverid);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        //[AllowAnonymous]
        [HttpPut("AcceptOrder")]
        public async Task<ActionResult<Order>> AccOrder([FromBody] AcceptOrderInput input)
        {
            var response = await _order.IsAcceptOrder(input);
            Console.WriteLine("Success Accept Order");
            return Ok(response);
        }

        //[AllowAnonymous]
        [HttpPut("FinishOrder")]
        public async Task<ActionResult<Order>> FinishOrder(FinishOrderInput input)
        {
            var response = await _order.IsFinishOrder(input);
            var wallet = _mapper.Map<Customer>(input);
            var result = await _customer.UpdateWalletCustomer(input.CustomerId, wallet);
            Console.WriteLine("Success Finish Order");
            return Ok(response);
            
        }

        //[AllowAnonymous]
        [HttpPut("UpdateWallet")]
        public async Task<ActionResult<WalletCustomerDto>> UpdateWallet(UpdateWalletDto updateWalletDto)
        {
            try
            {
                var driver = _mapper.Map<Customer>(updateWalletDto);
                var result = await _customer.UpdateWalletCustomer(updateWalletDto.CustomerId, driver);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
