namespace Meli.DataAccess.Interfaces;
    public interface IConnectionFactory
    {
    /// <summary>
    /// Gets a value indicating whether this instance is ready.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is ready; otherwise, <c>false</c>.
    /// </value>
    bool IsReady { get; }

    /// <summary>
    /// Setups the SQL configuration.
    /// </summary>
    /// <param name="sqlConnectionConfig">The SQL connection configuration.</param>
    void SetupSqlConfig(string sqlConnectionString);
    }

