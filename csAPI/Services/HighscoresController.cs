using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using csAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace csAPI.Services
{
    /// <summary>
    /// Handles highscores for quiz app
    /// </summary>
    [Route("api/[controller]")]
    public class HighscoresController : Controller
    {
        private String connetionString = Configuration.ConnectionStrings.dataBaseName + @"Initial Catalog=HighScore;Integrated Security=True";
        private SqlConnection cnn;
        private List<Highscore> getScoresForCategory(string category)
        {
            cnn = new SqlConnection(connetionString);
            List<Highscore> result = new List<Highscore>();
            try
            {
                cnn.Open();
                SqlCommand sql = new SqlCommand(@"SELECT * FROM quizhighscore 
WHERE category=@category 
ORDER BY score DESC", cnn);
                sql.Parameters.AddWithValue("@category", category);
                SqlDataReader reader = sql.ExecuteReader();
                // add all data into ordered list
                while (reader.Read())
                {
                    Highscore scoreTemp = new Highscore();
                    scoreTemp.user = reader["user"].ToString();
                    scoreTemp.score = Convert.ToDouble(reader["score"].ToString());
                    scoreTemp.category = reader["category"].ToString();
                    scoreTemp.difficulty = Convert.ToInt16(reader["difficulty"].ToString());
                    scoreTemp.id = Convert.ToInt32(reader["id"].ToString());
                    result.Add(scoreTemp);
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
        private void insertScore(Highscore score)
        {
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                SqlCommand sql = new SqlCommand(@"INSERT INTO quizhighscore
(category, [user], difficulty, score) VALUES 
(@category, @user, @difficulty, @score)", cnn);
                sql.Parameters.AddWithValue("@category", score.category);
                sql.Parameters.AddWithValue("@user", score.user);
                sql.Parameters.AddWithValue("@difficulty", score.difficulty);
                sql.Parameters.AddWithValue("@score", score.score);
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void deleteScore(int id)
        {
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                SqlCommand sql = new SqlCommand("DELETE FROM quizhighscore WHERE id=@id", cnn);
                sql.Parameters.AddWithValue("@id", id);
                sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        /// <summary>
        /// Returns List of Highscores
        /// </summary>
        [HttpGet]
        public List<Highscore> Get(String category)
        {
            if (!(category is String) || category.Length < 1) {
                Response.StatusCode = 400;
                return new List<Highscore>();
            }
            return getScoresForCategory(category);
        }

        /// <summary>
        /// Submits new highscore if possible
        /// </summary>
        [HttpPost]
        public String Post([FromBody]Highscore highscore)
        {
            if (!(highscore is Highscore))
            {
                Response.StatusCode = 400;
                return "invalid highscore";
            }
            List<Highscore> highscores = getScoresForCategory(highscore.category);
            if (highscores.Count < 10)
            {
                insertScore(highscore);
                return "score added";
            }
            for (int i = 0; i < highscores.Count; i++)
            {
                if (highscores[i].score < highscore.score)
                {
                    insertScore(highscore);
                    if (highscores.Count == 10)
                    {
                        deleteScore(highscores[9].id);
                    }
                    return "score altered";
                }
            }
            return "insufficient score";
        }
    }
}
