using System;

namespace csAPI.Models
{
    /// <summary>
    /// Quiz highscores
    /// </summary>
    public class Highscore
    {
        /// <summary>
        /// Gets the creation time.
        /// </summary>
        public DateTime CreateAd => DateTime.UtcNow;
        /// <summary>
        /// Score. Higher is better
        /// </summary>
        public double score { get; set; }

        /// <summary>
        /// Gets or sets user
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// Gets or sets category
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// Gets or sets difficulty
        /// </summary>
        public Int16 difficulty { get; set; }

        /// <summary>
        /// Gets or sets id to update or delete values
        /// </summary>
        public int id { get; set; }
    }
}
