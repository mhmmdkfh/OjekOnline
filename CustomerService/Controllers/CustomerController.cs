using CustomerService.Data;
using CustomerService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customer;

        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }
        [HttpGet]
        public String Get()
        {
            return "api works";
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterInput body)
        {
            Console.WriteLine("halo");
            try
            {
                await _customer.Register(body);
                return Ok($"Registrasi user {body.Username} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginInput body)
        {
            try
            {
                var res = await _customer.Login(body);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("CheckOrderFee")]
        public async Task<ActionResult> CheckOrderFee([FromBody] LoginInput body)
        {
            try
            {
                // await _customer.Register(body);
                return Ok($"Masih dummy ges.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("Order")]
        public async Task<ActionResult> Order([FromBody] LoginInput body)
        {
            try
            {
                // await _customer.Register(body);
                return Ok($"Masih dummy ges.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TopUp")]
        public async Task<ActionResult<TopUpResponse>> TopUp([FromBody] TopUpRequest request)
        {
            try
            {

                var response = await _customer.TopUp(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ViewSaldo")]
        public async Task<ActionResult<ViewSaldoResponse>> ViewSaldo()
        {
            try
            {
                var response = await _customer.ViewSaldo();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("ViewOrderHistory")]
        public async Task<ActionResult> ViewOrderHistory([FromBody] LoginInput body)
        {
            try
            {
                // await _customer.Register(body);
                return Ok($"Masih dummy ges.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
