namespace Meli.DataAccess.Interfaces;

using System;
using System.Security.Principal;

public interface IRepositoryFactory
{
    ICoupon ICoupon { get; set; }
    IProduct IProduct { get; set; }
    IRepository<TEntity> CreateRepository<TEntity>()
            where TEntity : class;
}

