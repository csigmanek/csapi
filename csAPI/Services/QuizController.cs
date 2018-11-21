using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace csAPI.Services
{
    /// <summary>
    /// Gets all Quizes
    /// </summary>
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private String connetionString = Configuration.ConnectionStrings.dataBaseName + @"Initial Catalog=HighScore;Integrated Security=True";
        private SqlConnection cnn;

        /// <summary>
        /// Returns List of Quizes
        /// </summary>
        [HttpGet]
        public List<String[]> Get()
        {
            cnn = new SqlConnection(connetionString);
            List<String[]> result = new List<String[]>();
            try
            {
                cnn.Open();
                SqlCommand sql = new SqlCommand(@"SELECT * FROM quizquestions", cnn);
                SqlDataReader reader = sql.ExecuteReader();
                // add all data into ordered list
                while (reader.Read())
                {
                    String[] temp = new String[4];
                    temp[0] = reader["category"].ToString();
                    temp[1] = reader["answers"].ToString();
                    temp[2] = reader["panswers"].ToString();
                    temp[3] = reader["ask"].ToString();
                    result.Add(temp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
    }
}
