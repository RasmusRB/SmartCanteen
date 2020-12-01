using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLib;
using ModelLib.Model;
using SmartCanteenREST.Managers;

namespace SmartCanteenREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //GET: api/<WeatherController>
        [HttpGet]
        public Weather GetNow()
        {
            return WeatherHandler.GetWeatherNow();
        }

        //GET: api/<WeatherController>/saves
        [HttpGet("saves")]
        public IEnumerable<Weather> Get()
        {
            return WeatherManager.GetAll();
        }

        //POST: api/<WeatherController>/saves
        [HttpPost("saves")]
        public bool Post([FromBody] Weather weather)
        {
            return WeatherManager.Create(weather);
        }

        //POST: api/<WeatherController>/saves/add
        [HttpPost("saves/add")]
        public bool Post()
        {
            return WeatherManager.Create(WeatherHandler.GetWeatherNow());
        }
    }
}
