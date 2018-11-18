using System;
using System.Collections.Generic;
using csAPI.Models;
using csAPI.Storage;
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
        /// <summary>
        /// Returns List of Highscores
        /// </summary>
        [HttpGet]
        public List<Highscore> Get()
        {
            return HighscoreStorage.highscores;
        }

        /// <summary>
        /// Submits new highscore if possible
        /// </summary>
        [HttpPost]
        public Boolean Post([FromBody]Highscore highscore)
        {
            if (!(highscore is Highscore))
            {
                Response.StatusCode = 400;
                return false;
            }
            Boolean inserted = false;
            for (int i = 0; i < HighscoreStorage.highscores.Count; i++)
            {
                if (HighscoreStorage.highscores[i].score < highscore.score)
                {
                    HighscoreStorage.highscores.Insert(i, highscore);
                    inserted = true;
                    break;
                }
            }
            if (HighscoreStorage.highscores.Count < 10 && !inserted)
            {
                HighscoreStorage.highscores.Insert(0, highscore);
                inserted = true;
            }
            else if (HighscoreStorage.highscores.Count > 10)
            {
                HighscoreStorage.highscores.RemoveAt(10);
            }
            return inserted;
        }
    }
}
