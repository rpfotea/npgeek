using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;


namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL: IWeatherDAL
    {
        private string connectionString;
        private const string SQL_GetWeathers = @"SELECT * FROM weather WHERE parkCode = @parkCode;";

        public WeatherSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> GetWeathers(string id)
        {
            List<Weather> output = new List<Weather>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetWeathers, conn);
                    cmd.Parameters.AddWithValue("@parkCode", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string parkcode = Convert.ToString(reader["parkcode"]);
                        int fivedayforecastvalue = Convert.ToInt32(reader["fivedayforecastvalue"]);
                        int low = Convert.ToInt32(reader["low"]);
                        int high = Convert.ToInt32(reader["high"]);
                        string forecast = Convert.ToString(reader["forecast"]);
                       
                        Weather weatherObj = new Weather();
                        weatherObj.ParkCode = parkcode;
                        weatherObj.FiveDayForecastValue = fivedayforecastvalue;
                        weatherObj.Low = low;
                        weatherObj.High = high;
                        weatherObj.Forecast = forecast;
                        
                        output.Add(weatherObj);
                    }
                    return output;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

    }
}