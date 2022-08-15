namespace Meli.DataAccess;
using DataAccess.Interfaces;
using Meli.Entities.Configuration;

public class ConnectionFactory : IConnectionFactory
{
    /// <summary>
    /// The connection configuration.
    /// </summary>
    private IDictionary<string, object>? connectionConfiguration;

    /// <inheritdoc/>
    public SqlConnectionSettings SqlConnectionConfig => this.GetValue<SqlConnectionSettings>(nameof(this.SqlConnectionConfig));

    /// <inheritdoc/>
    public string NoSqlConnectionString => this.GetValue<string>(nameof(this.NoSqlConnectionString));

    /// <inheritdoc/>
    public bool IsReady => this.connectionConfiguration != null;

    /// <inheritdoc/>
    public void SetupSqlConfig(SqlConnectionSettings sqlConnectionConfig)
    {
        this.connectionConfiguration ??= new Dictionary<string, object>();

        var key = nameof(this.SqlConnectionConfig);
        if (this.connectionConfiguration.ContainsKey(key))
        {
            this.connectionConfiguration[key] = sqlConnectionConfig;
        }
        else
        {
            this.connectionConfiguration.Add(key, sqlConnectionConfig);
        }
    }

    /// <inheritdoc/>
    public void SetupStorageConnection(string storageConnectionString)
    {
        this.connectionConfiguration ??= new Dictionary<string, object>();

        var key = nameof(this.NoSqlConnectionString);
        if (this.connectionConfiguration.ContainsKey(key))
        {
            this.connectionConfiguration[key] = storageConnectionString;
        }
        else
        {
            this.connectionConfiguration.Add(key, storageConnectionString);
        }
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="key">The key.</param>
    /// <returns>The configuration value.</returns>
    private TValue GetValue<TValue>(string key)
    {
        if (this.connectionConfiguration != null && this.connectionConfiguration.ContainsKey(key))
        {
            return (TValue)this.connectionConfiguration[key];
        }

        return default(TValue);
    }
}
