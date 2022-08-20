using System;
namespace Meli.Entities.Configuration
{
    public class SqlConnectionSettings
    {
        /// <summary>
        /// Gets or sets the service bus address.
        /// </summary>
        /// <value>
        /// The service bus address.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets the database URL.
        /// </summary>
        /// <value>
        /// The database URL.
        /// </value>
        public string ResourceUrl => $"https://database.windows.net/";

        /// </summary>
        /// <value>
        /// The command timeout in secs.
        /// </value>
        public int? CommandTimeoutInSecs { get; set; }
    }
}

