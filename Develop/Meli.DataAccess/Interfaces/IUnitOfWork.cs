using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Threading;

namespace Meli.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Gets the product repository.
        /// </summary>
        /// <value>
        /// The Product repository.
        /// </value>
        IProductRepository ProductRepository { get; }

        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// The repository.
        /// </returns>
        IRepository<TEntity> CreateRepository<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>1, when saved successfully, 0 otherwise.</returns>
        Task<int> SaveAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}

