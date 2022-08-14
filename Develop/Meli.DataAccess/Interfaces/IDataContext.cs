namespace Meli.DataAccess.Interfaces;

using System;

public interface IDataContext : IDisposable
{
    /// <summary>
    /// Saves the context asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Number of rows effected.
    /// </returns>
    Task<int> SaveAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Clears this instance.
    /// </summary>
    void Clear();
}

