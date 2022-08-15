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
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Gets the database URL.
        /// </summary>
        /// <value>
        /// The database URL.
        /// </value>
        public string ResourceUrl => $"https://database.windows.net/";

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        public string? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name of the queue.
        /// </summary>
        /// <value>
        /// The name of the queue.
        /// </value>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// Gets or sets the retry interval in secs.
        /// </summary>
        /// <value>
        /// The retry interval in secs.
        /// </value>
        public int RetryIntervalInSecs { get; set; }

        /// <summary>
        /// Gets or sets the command timeout in secs.
        /// </summary>
        /// <value>
        /// The command timeout in secs.
        /// </value>
        public int? CommandTimeoutInSecs { get; set; }
    }
}

