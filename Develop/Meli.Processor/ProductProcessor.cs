using Meli.DataAccess.Interfaces;
using Meli.Entities;
using Meli.Processor.Interfaces;
using Meli.Core;
namespace Meli.Processor;
public class ProductProcessor : IProductProcessor
{
    // private readonly IUnitOfWorkFactry unitOfWorkFactory;
    // private readonly IUnitOfWork unitOfWork;
    private readonly IRepositoryFactory repositoryFactory;

    private readonly IUnitOfWorkFactory unitOfWorkFactory;

    private readonly IUnitOfWork unitOfWork;

    public ProductProcessor(IRepositoryFactory _repositoryFactory, IUnitOfWork _unitOfWork, IUnitOfWorkFactory _unitOfWorkFactory)
    {
        this.repositoryFactory = _repositoryFactory;
        this.unitOfWorkFactory = _unitOfWorkFactory;
        this.unitOfWork = unitOfWorkFactory.GetUnitOfWork();
    }
    public async Task<IEnumerable<Product>> GetMostLikedProductsAsync()
    {
        var repository = this.repositoryFactory.ProductRepository;
        return await repository.GetMustLikedProducts();
    }

    public async Task CreatePruductAsync(IEnumerable<Product> products)
    {
        ArgumentValidators.ThrowIfNull(products, nameof(products));
        var parameters = new Dictionary<string, object>
            {
                { "@Products", products },
            };
        await this.unitOfWork.CreateRepository<Product>().ExecuteAsync("usp_Sp",parameters);
    }
}

