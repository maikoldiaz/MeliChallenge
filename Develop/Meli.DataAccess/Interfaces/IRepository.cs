namespace Meli.DataAccess.Interfaces;

using System;
using System.Linq.Expressions;
using System.Security.Principal;

public interface IRepository<TEntity>
        where TEntity : class
{
    /// <summary>
    /// Inserts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Insert(TEntity entity);

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>
    /// The identifier asynchronously.
    /// </returns>
    Task<TEntity> GetByIdAsync(object id);

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>
    /// The list of all entities asynchronously.
    /// </returns>
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties);

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <typeparam name="TResult">The type of other entity.</typeparam>
    /// <param name="predicate">The predicate.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>The list of all entities.</returns>
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TResult, bool>> predicate, params string[] includeProperties)
        where TResult : class;

    /// <summary>
    /// Executes the asynchronous.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <param name="data">The data.</param>
    /// <returns>The task.</returns>
    Task ExecuteAsync(object args, IDictionary<string, object> data);

    /// <summary>
    /// Executes the query asynchronous.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <param name="data">The data.</param>
    /// <returns>The Result.</returns>
    Task<IEnumerable<TEntity>> ExecuteQueryAsync(object args, IDictionary<string, object> data);
}

