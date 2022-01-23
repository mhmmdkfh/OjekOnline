using AutoMapper;
using DriverService.Data;
using DriverService.Dtos;
using DriverService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public DriversController(IDriver driver, IMapper mapper)
        {
            _mapper = mapper;
            _driver = driver;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> Get()
        {
            var drivers = await _driver.GetAll();
            return Ok(drivers);
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<Driver>> Get(int id)
        {
            try
            {
                var result = await _driver.GetById(id.ToString());
                // if (result == null)
                //     return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }*/
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CreateDriverDto>> Registration(CreateDriverDto obj)
        {
            try
            {
                var driver = _mapper.Map<Driver>(obj);
                var result = await _driver.Registration(driver);
                return Ok($"Driver registration {driver.FullName} is successful and waiting for admin to activate your account");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<DriverTokenDto>> LoginDriver(LoginDriverDto login)
        {
            var driver = await _driver.Login(login.Email, login.Password);
            return Ok(driver);
        }

        [HttpGet("Profile")]
        public ActionResult<DriverDto> ViewProfile()
        {
            try
            {

                Console.WriteLine($"--> Getting Driver With Email/Username: {_driver.ViewProfile().Email}");

                var driver = _driver.ViewProfile();
                if (driver != null)
                {
                    return Ok(_mapper.Map<DriverDto>(driver));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Wallet")]
        public ActionResult<WalletDriverDto> ViewWallet()
        {
            try
            {
                Console.WriteLine($"--> Getting Driver Wallet With Email/Username: {_driver.ViewProfile().Email}");
                var driver = _driver.ViewWallet();
                if (driver != null)
                {
                    return Ok(_mapper.Map<WalletDriverDto>(driver));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("SetLocation")]
        public async Task<ActionResult<SetLocationDriverDto>> SetLocation(SetLocationDriverDto setLocation)
        {
            try
            {
                var driver = _mapper.Map<Driver>(setLocation);
                var result = await _driver.SetLocation(driver);
                var driverdto = _mapper.Map<SetLocationDriverDto>(result);
                return Ok(driverdto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Approve")]
        public async Task<ActionResult> ApproveDriver(LockDriverInput input)
        {
            try
            {
                await _driver.Approve(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Lock")]
        public async Task<ActionResult> LockDriver(LockDriverInput input)
        {
            try
            {
                await _driver.Lock(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
