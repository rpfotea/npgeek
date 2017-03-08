using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class NpGeekSqlDAL : INpGeekDAL
    {
        private string connectionString;
        private const string SQL_GetParks = "SELECT * FROM park;";
        //private const string SQL_GetPark = @"SELECT * FROM park WHERE park_parkCode = (@park_parkCode);";

        public NpGeekSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetParks()
        {
            List<Park> output = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string parkcode = Convert.ToString(reader["parkcode"]);
                        string parkname = Convert.ToString(reader["parkname"]);
                        string state = Convert.ToString(reader["state"]);
                        int acreage = Convert.ToInt32(reader["acreage"]);
                        int elevationinfeet = Convert.ToInt32(reader["elevationinfeet"]);
                        double milesoftrail = Convert.ToDouble(reader["milesoftrail"]);
                        string climate = Convert.ToString(reader["climate"]);
                        int yearfounded = Convert.ToInt32(reader["yearfounded"]);
                        int annualvisitorcount = Convert.ToInt32(reader["annualvisitorcount"]);
                        string inspirationalquote = Convert.ToString(reader["inspirationalquote"]);
                        string inspirationalquotesource = Convert.ToString(reader["inspirationalquotesource"]);
                        string parkdescription = Convert.ToString(reader["parkdescription"]);
                        int entryfee = Convert.ToInt32(reader["entryfee"]);
                        int numberofanimalspecies = Convert.ToInt32(reader["numberofanimalspecies"]);

                        Park parkObj = new Park();
                        parkObj.ParkCode = parkcode;
                        parkObj.ParkName = parkname;
                        parkObj.State = state;
                        parkObj.Acreage = acreage;
                        parkObj.ElevationInFeet = elevationinfeet;
                        parkObj.MilesOfTrail = milesoftrail;
                        parkObj.Climate = climate;
                        parkObj.YearFounded = yearfounded;
                        parkObj.AnnualVisitorCount = annualvisitorcount;
                        parkObj.InspirationalQuote = inspirationalquote;
                        parkObj.InspirationalQuoteSource = inspirationalquotesource;
                        parkObj.ParkDescription = parkdescription;
                        parkObj.EntryFee = entryfee;
                        parkObj.NumberOfAnimalSpecies = numberofanimalspecies;

                        output.Add(parkObj);

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