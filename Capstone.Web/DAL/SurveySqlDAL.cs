using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveyDAL
    {
        private string connectionString;
        private const string SQL_InsertSurvey = "INSERT INTO survey_result VALUES (@park_code, @email_address, @state, @activity_level);";
        private const string SQL_GetAllSurveys = "SELECT park.parkName, COUNT(survey_result.parkCode) AS Total_Survey_Park FROM survey_result INNER JOIN park ON survey_result.parkCode=park.parkCode GROUP BY park.parkName ORDER BY Total_Survey_Park DESC ";

        public SurveySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public bool SaveSurvey(Survey newSurvey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertSurvey, conn);
                    cmd.Parameters.AddWithValue("@park_code", newSurvey.ParkCode );
                    cmd.Parameters.AddWithValue("@email_address", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activity_level", newSurvey.ActivityLevel);
                   

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
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


