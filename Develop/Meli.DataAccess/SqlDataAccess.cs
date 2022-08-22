using System.Data;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Meli.DataAccess.Interfaces;
using Microsoft.Data.SqlClient;

namespace Meli.DataAccess;
    public class SqlDataAccess<TEntity> : ISqlDataAccess<TEntity>
            where TEntity : class
    {
        private readonly ISqlDataContext dataContext;
        private readonly DbSet<TEntity> dataset;

        public SqlDataAccess(ISqlDataContext sqlDataContext)
        {
            this.dataContext = sqlDataContext;
            this.dataset = this.dataContext.Set<TEntity>();
        }
        public DbSet<TEntity> EntitySet()
        {
            return this.dataset;
        }

        public Task ExecuteAsync(object args, IDictionary<string, object> data)
        {
            var sql = BuildSql(args.ToString()!, data.Keys);
            var parameters = data.Select(BuildParameters);

            return this.dataContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TResult, bool>> predicate, params string[] includeProperties)
            where TResult : class
        {
            IQueryable<TResult> query = this.Set<TResult>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.dataset.FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            this.dataset.Add(entity);
        }

        public DbSet<T> Set<T>() where T : class
        {
            return this.dataContext.Set<T>();
        }

        private static string BuildSql(string name, IEnumerable<string> keys)
        {
            var sb = new StringBuilder();

            // build input parameters
            var input = keys.Where(k => !k.StartsWith("@out_", StringComparison.OrdinalIgnoreCase));
            sb.Append(name).Append(' ').Append(string.Join(",", input));

            // build output parameters
            var output = keys.Where(k => k.StartsWith("@out_", StringComparison.OrdinalIgnoreCase)).ToList();
            output.ForEach(o =>
            {
                sb.Append(',').Append(o).Append(' ').Append("OUTPUT");
            });

            return sb.ToString();
        }
        private static SqlParameter BuildParameters(KeyValuePair<string, object> parameter)
        {
            var direction = parameter.Key.StartsWith("@out_", StringComparison.OrdinalIgnoreCase) ? ParameterDirection.Output : ParameterDirection.Input;
            var p = new SqlParameter
            {
                ParameterName = parameter.Key,
                Direction = direction,
            };

            // Output parameters of int type are only supported as of now
            if (direction == ParameterDirection.Output)
            {
                p.SqlDbType = SqlDbType.Int;
            }

            if (direction == ParameterDirection.Input)
            {
                p.Value = parameter.Value ?? DBNull.Value;
            }

            if (!(parameter.Value is DataTable dt))
            {
                return p;
            }

            p.TypeName = dt.TableName;
            p.SqlDbType = SqlDbType.Structured;

            return p;
        }

        public async Task<IEnumerable<TEntity>> ExecuteQueryAsync(object args, IDictionary<string, object> data)
        {
            var sql = BuildSql(args.ToString()!, data.Keys);
            var parameters = data.Select(BuildParameters);

            return await this.dataContext.Set<TEntity>().FromSqlRaw(sql, parameters.ToArray<object>()).ToListAsync();
        }

        public void InsertAll(IEnumerable<TEntity> entities)
        {
            this.dataset.AddRange(entities);
        }
    }