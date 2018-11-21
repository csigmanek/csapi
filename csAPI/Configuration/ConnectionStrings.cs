using System;

namespace csAPI.Configuration
{
    /// <summary>
    /// global settings that differ in prod and dev
    /// </summary>
    public class ConnectionStrings 
    {
        /// <summary>
        /// name of database
        /// </summary>
        public static String dataBaseName = @"Data Source=DESKTOP-25CISML\SQLEXPRESS;";

        // old & deprecated
        public string ApiDb { get; set; }
        public string Api2Db { get; set; }
    }
}