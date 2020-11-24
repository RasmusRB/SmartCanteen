using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Weather
    {
        private string _location;
        private double _temperature;
        private string _weatherProg;
        private DateTime _dateTime;

        public Weather()
        {

        }

        public Weather(string location, double temperature, string weatherProg, DateTime dateTime)
        {
            _location = location;
            _temperature = temperature;
            _weatherProg = weatherProg;
            _dateTime = dateTime;
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => _dateTime = value;
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

        public string WeatherProg
        {
            get => _weatherProg;
            set => _weatherProg = value;
        }

        public override string ToString()
        {
            return $"{nameof(_location)}: {_location}, {nameof(_temperature)}: {_temperature}, {nameof(_weatherProg)}: {_weatherProg}, {nameof(_dateTime)}: {_dateTime}";
        }
    }
}
