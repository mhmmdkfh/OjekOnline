using AutoMapper;
using DriverService.Data;
using DriverService.Dtos;
using DriverService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DriverService.Controllers
{
    [Route("api/[controller]")]
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

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> Get()
        {
            var drivers = await _driver.GetAll();
            return Ok(drivers);
        }*/

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

        [HttpPost]
        public async Task<ActionResult<CreateDriverDto>> Registration(CreateDriverDto obj)
        {
            try
            {
                var driver = _mapper.Map<Driver>(obj);
                var result = await _driver.Registration(driver);
                return Ok($"Regristrasi driver {driver.FullName} berhasil dan menunggu admin mengaktifkan akun anda");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
