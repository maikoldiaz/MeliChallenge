using System;
using System.Security.Principal;
using Meli.DataAccess.Interfaces;
using System.Threading.Tasks;
using System.Threading;
namespace Meli.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IDataContext dataContext;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        public IProductRepository ProductRepository => this.repositoryFactory.ProductRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public UnitOfWork(IDataContext dataContext, IRepositoryFactory repositoryFactory)
        {
            if (dataContext == null) throw new NullReferenceException(nameof(dataContext));
            if (dataContext == null) throw new NullReferenceException(nameof(repositoryFactory));
            this.dataContext = dataContext;
            this.repositoryFactory = repositoryFactory;
        }

        public IRepository<TEntity> CreateRepository<TEntity>()
                where TEntity : class
        {
            return this.repositoryFactory.CreateRepository<TEntity>();
        }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// 1, when saved successfully, 0 otherwise.
        /// </returns>
        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return this.dataContext.SaveAsync(cancellationToken);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.dataContext.Dispose();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.dataContext.Clear();
        }
    }
}