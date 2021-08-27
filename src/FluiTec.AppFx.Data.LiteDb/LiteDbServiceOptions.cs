using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Data.LiteDb
{
    [ConfigurationKey("LiteDb")]
    public class LiteDbServiceOptions
    {
        /// <summary>	Gets or sets the filename of the database file. </summary>
        /// <value>	The filename of the database file. </value>
        public string DbFileName { get; set; }

        /// <summary>   Gets the full pathname of the full database file. </summary>
        /// <value> The full pathname of the full database file. </value>
        public string FullDbFilePath => ApplicationFolder != null ? System.IO.Path.Combine(ApplicationFolder, DbFileName) : DbFileName;

        /// <summary>	Gets or sets the pathname of the application folder. </summary>
        /// <value>	The pathname of the application folder. </value>
        public string ApplicationFolder { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object use singleton connection.
        /// </summary>
        /// <value>	True if use singleton connection, false if not. </value>
        public bool UseSingletonConnection { get; set; }
    }
}