using System;
using Meli.DataAccess.Interfaces;
using Meli.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meli.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ISqlDataAccess<Product> sqlDataAccess;
        public ProductRepository(ISqlDataAccess<Product> sqlDataAccess)
            : base(sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }
        private DbSet<Product> Products => this.sqlDataAccess.Set<Product>();

        public async Task<IEnumerable<Product>> GetMustLikedProducts()
        {
            return await Task<IEnumerable<Product>>.Run(() =>
            {
                return this.Products.AsQueryable().OrderByDescending(x => x.likesNumber).Take(5);
            });
        }
    }
}