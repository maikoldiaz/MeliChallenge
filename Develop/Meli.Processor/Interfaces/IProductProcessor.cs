using System;
using Meli.Entities;
namespace Meli.Processor.Interfaces;
public interface IProductProcessor
{
    Task<IEnumerable<Product>> GetMostLikedProductsAsync();

    Task CreatePruductAsync(IEnumerable<Product> products);
}

