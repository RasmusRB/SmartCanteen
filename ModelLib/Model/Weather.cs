using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Weather
    {
        private string _location;
        private double _temperature;        
        private DateTime _dateTime;
        private double _rain;
        private double _sun;
        private double _wind;

        public Weather()
        {

        }

        public Weather(string location, double temperature, double rain, double sun, double wind, DateTime dateTime)
        {
            _location = location;
            _temperature = temperature;
            _rain = rain;
            _sun = sun;
            _wind = wind;
            _dateTime = dateTime;
        }

        public string Location
        {
            get => _location;
            set => _location = value;
        }

        public double Temperature
        {
            get => _temperature;
            set => _temperature = value;
        }

        public double Rain
        {
            get => _rain;
            set => _rain = value;
        }

        public double Sun
        {
            get => _rain;
            set => _rain = value;
        }

        public double Wind
        {
            get => _rain;
            set => _rain = value;
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => _dateTime = value;
        }

        public override string ToString()
        {
            return $"{nameof(_location)}: {_location}, " +
                $"{nameof(_temperature)}: {_temperature}, " +
                $"{nameof(_rain)}: {_rain}, " +
                $"{nameof(_sun)}: {_sun}, " +
                $"{nameof(_wind)}: {_wind}, " +
                $"{nameof(_dateTime)}: {_dateTime}";
        }
    }
}
