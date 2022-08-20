namespace Meli.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The sql data access.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="Meli.DataAccess.Interfaces.IDataAccess{TEntity}" />
public interface ISqlDataAccess<TEntity> : IDataAccess<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Sets this instance.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns>The DB Set.</returns>
    DbSet<TEntity> EntitySet();

    /// <summary>
    /// Sets this instance.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <returns>The DB Set.</returns>
    DbSet<T> Set<T>()
        where T : class;
}