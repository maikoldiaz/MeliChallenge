using System.Linq.Expressions;
using Meli.DataAccess.Interfaces;
namespace Meli.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
            where TEntity : class
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly IDataAccess<TEntity> dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public Repository(IDataAccess<TEntity> dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public Task ExecuteAsync(object args, IDictionary<string, object> data)
        {
            return this.dataAccess.ExecuteAsync(args, data);
        }

        public Task<IEnumerable<TEntity>> ExecuteQueryAsync(object args, IDictionary<string, object> data)
        {
            return this.dataAccess.ExecuteQueryAsync(args, data);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            return this.dataAccess.GetAllAsync(predicate, includeProperties);
        }

        public Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TResult, bool>> predicate, params string[] includeProperties) where TResult : class
        {
            return this.dataAccess.GetAllAsync(predicate, includeProperties);
        }

        public Task<TEntity> GetByIdAsync(object id)
        {
            return this.dataAccess.GetByIdAsync(id);
        }

        public void Insert(TEntity entity)
        {
            this.dataAccess.Insert(entity);
        }

        public void InsertAll(IEnumerable<TEntity> entities)
        {
            this.dataAccess.InsertAll(entities);
        }
    }
}