using Meli.DataAccess.Interfaces;
using Meli.DataAccess;
using Meli.Entities;

namespace Meli.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly ISqlDataContext dataContext;

        public IProductRepository ProductRepository => new ProductRepository(new SqlDataAccess<Product>(this.dataContext));

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactory" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="businessContext">The business context.</param>
        public RepositoryFactory(ISqlDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IRepository<TEntity> CreateRepository<TEntity>()
                where TEntity : class
        {
            return new Repository<TEntity>(new SqlDataAccess<TEntity>(this.dataContext));
        }
    }
}