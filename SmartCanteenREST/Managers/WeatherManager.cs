using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ModelLib.Model;

namespace SmartCanteenREST.Managers
{
    public static class WeatherManager
    {
        private const string ConnString = "Data Source=smartcanteen-db-erver.database.windows.net;Initial Catalog=SmartCanteen-DB;User ID=smadmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GetAllString = "SELECT * FROM Weather";
        private const string CreateString = "INSERT INTO Weather ( Temperature, Weather_date, Rain, Sun, Wind, Location ) VALUES ( @Temperature, @Weather_date, @Rain, @Sun, @Wind, @Location )";

        /*
         * Only GET & POST implemented as we do not wish to
         * DELETE or UPDATE weather data received from dmi
         */

        // Reads weather
        private static Weather ReadNext(SqlDataReader reader)
        {
            Weather weather = new Weather
            {
                Temperature = reader.GetDouble(1),
                DateTime = reader.GetDateTime(2),
                Rain = reader.GetDouble(3),
                Sun = reader.GetDouble(4),
                Wind = reader.GetDouble(5),
                Location = reader.GetString(6)
            };

            return weather;
        }

        // GETS all weather data
        public static IList<Weather> GetAll()
        {
            List<Weather> weatherData = new List<Weather>();

            using SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            using SqlCommand cmd = new SqlCommand(GetAllString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                weatherData.Add(ReadNext(reader));
            }

            return weatherData;
        }

        // Creates weather data
        public static bool Create(Weather weather)
        {
            using SqlConnection conn = new SqlConnection(ConnString);
            conn.Open();

            using SqlCommand cmd = new SqlCommand(CreateString, conn);
            cmd.Parameters.AddWithValue("@Temperature", weather.Temperature);
            cmd.Parameters.AddWithValue("@Weather_date", weather.DateTime);
            cmd.Parameters.AddWithValue("@Rain", weather.Rain);
            cmd.Parameters.AddWithValue("@Sun", weather.Sun);
            cmd.Parameters.AddWithValue("@Wind", weather.Wind);
            cmd.Parameters.AddWithValue("@Location", weather.Location);

            int rows = cmd.ExecuteNonQuery();
            return rows == 1;
        }
    }
}
