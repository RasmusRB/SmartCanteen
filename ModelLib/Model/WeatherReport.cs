using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class WeatherReport
    {
        public double Temperature { get; set; }

        public double WindSpeed { get; set; }

        public double Precipitation { get; set; }

        public double Sunshine { get; set; }

        public WeatherReport()
        {
        }
    }
}
