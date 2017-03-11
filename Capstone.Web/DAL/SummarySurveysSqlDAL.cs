using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SummarySurveysSqlDAL : ISummarySurveysDAL
    {
        private string connectionString;
        private const string SQL_GetAllSurveys = "SELECT park.parkName, park.parkCode, COUNT(survey_result.parkCode) AS Total_Survey_Park FROM survey_result INNER JOIN park ON survey_result.parkCode=park.parkCode GROUP BY park.parkName, park.parkCode ORDER BY Total_Survey_Park DESC ";

        public SummarySurveysSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<SummarySurveys> GetSummarySurveys()
        {
            List<SummarySurveys> output = new List<SummarySurveys>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllSurveys, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string parkname = Convert.ToString(reader["parkname"]);
                        string parkcode = Convert.ToString(reader["parkcode"]);
                        string totalsurvey = Convert.ToString(reader["Total_Survey_Park"]);

                        SummarySurveys parkObj = new SummarySurveys();
                        parkObj.ParkCode = parkcode;
                        parkObj.ParkName = parkname;
                        parkObj.TotalSurveys = Convert.ToInt32(totalsurvey);

                        output.Add(parkObj);
                    }
                    return output;
                }
            }
            catch (SqlException ex)
            {
                // Error Logging that a problem occurred, don't show the user
                throw;
            }
        }
    }
}