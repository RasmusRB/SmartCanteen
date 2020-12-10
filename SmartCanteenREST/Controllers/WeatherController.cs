using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
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


        public WeatherController()
        {
            _timer.Elapsed += SaveWeather; // Event to do your tasks.
            ResetTimer();
        }

        private readonly Timer _timer = new Timer();

        private void ResetTimer()
        {
            _timer.Stop();

            //Gets todays datetime at 12:00
            //DateTime dateTime = DateTime.Now.Date.AddHours(12);
            //Adds a day if we already passed 12:00
            //if (DateTime.Now.TimeOfDay > TimeSpan.FromHours(12))
                //dateTime = dateTime.AddDays(1);

            DateTime dateTime = DateTime.Now.AddMinutes(1); //Testing datetime

            _timer.Interval = dateTime.Subtract(DateTime.Now).TotalMilliseconds;
            _timer.Start();
        }

        private void SaveWeather(object sender, ElapsedEventArgs e)
        {
            Post();
            ResetTimer();
        }
    }
}
