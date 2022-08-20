
using System;
using System.Security.Principal;

namespace Meli.DataAccess.Interfaces
{
    public interface IRepositoryFactory
    {
        IProductRepository ProductRepository { get; }
        IRepository<TEntity> CreateRepository<TEntity>()
                where TEntity : class;
    }
}
