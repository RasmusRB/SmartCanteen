using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Weather
    {
        private string _location;
        private double _temperature;
        private bool _rain;
        private DateTime _dateTime;

        public Weather()
        {

        }

        public Weather(string location, double temperature, bool rain, DateTime dateTime)
        {
            _location = location;
            _temperature = temperature;
            _rain = rain;
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

        public bool Rain
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
            return $"{nameof(_location)}: {_location}, {nameof(_temperature)}: {_temperature}, {nameof(_rain)}: {_rain}, {nameof(_dateTime)}: {_dateTime}";
        }
    }
}
