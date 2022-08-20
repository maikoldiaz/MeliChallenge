namespace Meli.DataAccess.Interfaces;
using Meli.Entities;
public interface IProductRepository : IRepository<Product>
{
    /// <summary>
    /// Gets the latest inventory product unique identifier.
    /// </summary>
    /// <param name="inventoryProductUniqueId">The inventory product unique identifier.</param>
    /// <returns>The product volume.</returns>
    Task<IEnumerable<Product>> GetMustLikedProducts();
}