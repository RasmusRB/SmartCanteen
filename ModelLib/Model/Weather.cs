using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Weather
    {
        private string _location;
        private int _temperature;
        private string _weatherProg;

        public Weather()
        {

        }

        public Weather(string location, int temperature, string weatherProg)
        {
            _location = location;
            _temperature = temperature;
            _weatherProg = weatherProg;
        }

        public string Location
        {
            get => _location;
            set => _location = value;
        }

        public int Temperature
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
            return $"{nameof(_location)}: {_location}, {nameof(_temperature)}: {_temperature}, {nameof(_weatherProg)}: {_weatherProg}";
        }
    }
}
