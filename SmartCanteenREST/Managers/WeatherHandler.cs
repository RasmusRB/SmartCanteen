using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using ModelLib.Model;

namespace SmartCanteenREST.Managers
{
    public static class WeatherHandler
    {
        //Gets the stored weather object or makes and new one if its 10 minutes old
        public static Weather GetWeatherNow()
        {
            if (_weatherReport != null && _weatherReport.DateTime.AddMinutes(10) > DateTime.Now)
                return _weatherReport;

            return GetWeatherFromDmi().Result;
        }

        #region DMI Code

        //Weather report
        private static Weather _weatherReport;
        
        //Test data so not to fetch from dmi when testing
        private const string TestData =
            "[{_id:ffc4b944-2fe4-11eb-af94-2ef478ae090b,parameterId:temp_soil,stationId:06174,timeCreated:1.606394615506413E15,timeObserved:1.6063944E15,value:8.1,{_id:ffc45a12-2fe4-11eb-92f6-b2b4096b36c0,parameterId:visib_mean_last10min,stationId:06174,timeCreated:1.60639461550396E15,timeObserved:1.6063944E15,value:42700.0,{_id:ffc401f2-2fe4-11eb-93a4-426eaeac18ea,parameterId:wind_speed,stationId:06174,timeCreated:1.60639461550168E15,timeObserved:1.6063944E15,value:4.4,{_id:ffc400c6-2fe4-11eb-83ea-2e6a3bbbd2de,parameterId:precip_dur_past10min,stationId:06174,timeCreated:1.606394615501648E15,timeObserved:1.6063944E15,value:0.0,{_id:ffc3ff7c-2fe4-11eb-93a4-426eaeac18ea,parameterId:wind_max,stationId:06174,timeCreated:1.60639461550162E15,timeObserved:1.6063944E15,value:7.3,{_id:ffc3fc52-2fe4-11eb-83ea-2e6a3bbbd2de,parameterId:leav_hum_dur_past10min,stationId:06174,timeCreated:1.606394615501556E15,timeObserved:1.6063944E15,value:0.0,{_id:ffc3fc7a-2fe4-11eb-93a4-426eaeac18ea,parameterId:wind_dir,stationId:06174,timeCreated:1.606394615501541E15,timeObserved:1.6063944E15,value:271.0,{_id:ffc3fa0e-2fe4-11eb-93a4-426eaeac18ea,parameterId:temp_grass,stationId:06174,timeCreated:1.606394615501474E15,timeObserved:1.6063944E15,value:9.1,{_id:ffc3f87e-2fe4-11eb-93a4-426eaeac18ea,parameterId:temp_dry,stationId:06174,timeCreated:1.606394615501434E15,timeObserved:1.6063944E15,value:9.2,{_id:ffc3f6d0-2fe4-11eb-93a4-426eaeac18ea,parameterId:temp_dew,stationId:06174,timeCreated:1.606394615501391E15,timeObserved:1.6063944E15,value:5.5,{_id:ffc3f4dc-2fe4-11eb-93a4-426eaeac18ea,parameterId:sun_last10min_glob,stationId:06174,timeCreated:1.606394615501344E15,timeObserved:1.6063944E15,value:9.0,{_id:ffc3ef96-2fe4-11eb-93a4-426eaeac18ea,parameterId:radia_glob,stationId:06174,timeCreated:1.606394615501235E15,timeObserved:1.6063944E15,value:207.0,{_id:ffc3e55a-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:pressure_at_sea,stationId:06174,timeCreated:1.606394615500949E15,timeObserved:1.6063944E15,value:1015.5,{_id:ffc3e348-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:pressure,stationId:06174,timeCreated:1.606394615500892E15,timeObserved:1.6063944E15,value:1012.9,{_id:ffc3e0aa-2fe4-11eb-882e-829b095a5aa0,parameterId:visibility,stationId:06174,timeCreated:1.606394615500854E15,timeObserved:1.6063944E15,value:40000.0,{_id:ffc3e0d2-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:precip_past10min,stationId:06174,timeCreated:1.606394615500838E15,timeObserved:1.6063944E15,value:0.0,{_id:ffc3d6fa-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:humidity,stationId:06174,timeCreated:1.606394615500636E15,timeObserved:1.6063944E15,value:78.0]";

        //Dictionary of weather locations links
        private static readonly Dictionary<string, string> Locations = new Dictionary<string, string>()
        {
            {"Tessebølle", "06174"},
            {"Holbæk Flyveplads", "06156"}
        };

        //DMI uri for fetching the weather
        private const string Uri =
            "https://dmigw.govcloud.dk/metObs/v1/observation?stationId=(insertId)&latest-10-minutes&api-key=7b8fcb17-1955-4edc-a7ff-035aca428556";

        //For assigning values
        private static readonly Dictionary<string, Action<double>> ParameterDictionary = new Dictionary<string, Action<double>>()
        {
            {"temp_dry", (x) => _weatherReport.Temperature = x},
            {"wind_speed", (x) => _weatherReport.Wind = x},
            {"precip_dur_past10min", (x) => _weatherReport.Rain = x},
            {"sun_last10min_glob", (x) => _weatherReport.Sun = x}

        };

        //Fetching data from dmi and making a weather object
        private static async Task<Weather> GetWeatherFromDmi()
        {
            _weatherReport = new Weather();
            
            string response = "";
            using HttpClient client = new HttpClient();
            foreach (var location in Locations)
            {
                string finalUri = Uri.Replace("(insertId)", location.Value);
                _weatherReport.Location = location.Key;
                response = await client.GetStringAsync(finalUri);
                if (!string.IsNullOrWhiteSpace(response))
                    break;
            }

            if (string.IsNullOrWhiteSpace(response))
                return null;

            response = response.Replace("\"", "");
            response = response.Replace("}", "");

            string[] parameterList = response.Split("parameterId:");

            foreach (string s in parameterList)
            {
                string parameter = s.Split(',')[0];
                if (ParameterDictionary.ContainsKey(parameter))
                {
                    string test = s.Split(',')[4].Split(':')[1];
                    double value = double.Parse(test, new CultureInfo("en"));
                    ParameterDictionary[parameter].Invoke(value);
                }
            }

            _weatherReport.DateTime = DateTime.Now;

            return _weatherReport;
        }

        #endregion
    }
}
