using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLib;
using ModelLib.Model;

namespace SmartCanteenREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //GET: api/<WeatherController>
        [HttpGet]
        public WeatherReport Get()
        {
            return WeatherHandler.GetWeatherReport();
        }
    }
}
