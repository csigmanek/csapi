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
        /// The score. Higher is better
        /// </summary>
        public double score { get; set; }

        /// <summary>
        /// Gets or sets UserName
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets info
        /// </summary>
        public string Info { get; set; }
    }
}
