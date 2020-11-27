using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ModelLib.Model;
using Newtonsoft.Json;

namespace ModelLib
{
    public static class WeatherHandler
    {
        private const string TestData =
            "[{_id:ffc4b944-2fe4-11eb-af94-2ef478ae090b,parameterId:temp_soil,stationId:06174,timeCreated:1.606394615506413E15,timeObserved:1.6063944E15,value:8.1,{_id:ffc45a12-2fe4-11eb-92f6-b2b4096b36c0,parameterId:visib_mean_last10min,stationId:06174,timeCreated:1.60639461550396E15,timeObserved:1.6063944E15,value:42700.0,{_id:ffc401f2-2fe4-11eb-93a4-426eaeac18ea,parameterId:wind_speed,stationId:06174,timeCreated:1.60639461550168E15,timeObserved:1.6063944E15,value:4.4,{_id:ffc400c6-2fe4-11eb-83ea-2e6a3bbbd2de,parameterId:precip_dur_past10min,stationId:06174,timeCreated:1.606394615501648E15,timeObserved:1.6063944E15,value:0.0,{_id:ffc3ff7c-2fe4-11eb-93a4-426eaeac18ea,parameterId:wind_max,stationId:06174,timeCreated:1.60639461550162E15,timeObserved:1.6063944E15,value:7.3,{_id:ffc3fc52-2fe4-11eb-83ea-2e6a3bbbd2de,parameterId:leav_hum_dur_past10min,stationId:06174,timeCreated:1.606394615501556E15,timeObserved:1.6063944E15,value:0.0,{_id:ffc3fc7a-2fe4-11eb-93a4-426eaeac18ea,parameterId:wind_dir,stationId:06174,timeCreated:1.606394615501541E15,timeObserved:1.6063944E15,value:271.0,{_id:ffc3fa0e-2fe4-11eb-93a4-426eaeac18ea,parameterId:temp_grass,stationId:06174,timeCreated:1.606394615501474E15,timeObserved:1.6063944E15,value:9.1,{_id:ffc3f87e-2fe4-11eb-93a4-426eaeac18ea,parameterId:temp_dry,stationId:06174,timeCreated:1.606394615501434E15,timeObserved:1.6063944E15,value:9.2,{_id:ffc3f6d0-2fe4-11eb-93a4-426eaeac18ea,parameterId:temp_dew,stationId:06174,timeCreated:1.606394615501391E15,timeObserved:1.6063944E15,value:5.5,{_id:ffc3f4dc-2fe4-11eb-93a4-426eaeac18ea,parameterId:sun_last10min_glob,stationId:06174,timeCreated:1.606394615501344E15,timeObserved:1.6063944E15,value:9.0,{_id:ffc3ef96-2fe4-11eb-93a4-426eaeac18ea,parameterId:radia_glob,stationId:06174,timeCreated:1.606394615501235E15,timeObserved:1.6063944E15,value:207.0,{_id:ffc3e55a-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:pressure_at_sea,stationId:06174,timeCreated:1.606394615500949E15,timeObserved:1.6063944E15,value:1015.5,{_id:ffc3e348-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:pressure,stationId:06174,timeCreated:1.606394615500892E15,timeObserved:1.6063944E15,value:1012.9,{_id:ffc3e0aa-2fe4-11eb-882e-829b095a5aa0,parameterId:visibility,stationId:06174,timeCreated:1.606394615500854E15,timeObserved:1.6063944E15,value:40000.0,{_id:ffc3e0d2-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:precip_past10min,stationId:06174,timeCreated:1.606394615500838E15,timeObserved:1.6063944E15,value:0.0,{_id:ffc3d6fa-2fe4-11eb-9631-66d6ed0f2ed8,parameterId:humidity,stationId:06174,timeCreated:1.606394615500636E15,timeObserved:1.6063944E15,value:78.0]";

        private static readonly List<string> Uris = new List<string>()
        {
            "https://dmigw.govcloud.dk/metObs/v1/observation?stationId=06174&latest-10-minutes&api-key=7b8fcb17-1955-4edc-a7ff-035aca428556", 
            "https://dmigw.govcloud.dk/metObs/v1/observation?stationId=06156&latest-10-minutes&api-key=7b8fcb17-1955-4edc-a7ff-035aca428556"
        };

        private static readonly Dictionary<string, Action<double>> ParameterDictionary = new Dictionary<string, Action<double>>()
        {
            {"temp_dry", (x) => _weatherReport.Temperature = x},
            {"wind_speed", (x) => _weatherReport.WindSpeed = x},
            {"precip_dur_past10min", (x) => _weatherReport.Precipitation = x},
            {"sun_last10min_glob", (x) => _weatherReport.Sunshine = x}

        };

        private static DateTime _reportExpires;
        private static WeatherReport _weatherReport;


        public static WeatherReport GetWeatherReport()
        {
            if (_reportExpires > DateTime.Now)
                return _weatherReport;

            _reportExpires = DateTime.Now.AddMinutes(10);
            return MakeWeatherReport().Result;
        }

        private static async Task<WeatherReport> MakeWeatherReport()
        {
            string response = "";
            //using HttpClient client = new HttpClient();
            //foreach (string uri in Uris)
            //{
            //    response = await client.GetStringAsync(uri);
            //    if (!string.IsNullOrWhiteSpace(response))
            //        break;
            //}

            //if (string.IsNullOrWhiteSpace(response))
            //    return null;

            response = TestData;

            response = response.Replace("\"", "");
            response = response.Replace("}", "");

            string[] parameterList = response.Split("parameterId:");

            _weatherReport = new WeatherReport();

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

            return _weatherReport;
        }
    }
}
