using System;
using Meli.DataAccess.Interfaces;
using Meli.Entities;
using Meli.Processor.Interfaces;
namespace Meli.Processor;
public class ProductProcessor : IProductProcessor
{
    // private readonly IUnitOfWorkFactry unitOfWorkFactory;
    // private readonly IUnitOfWork unitOfWork;
    private readonly IRepositoryFactory repositoryFactory;

    public ProductProcessor(IRepositoryFactory _repositoryFactory)
    {
        this.repositoryFactory = _repositoryFactory;
        // this.unitOfWorkFactory = _unitOfWorkFactory;
        // this.unitOfWork = unitOfWorkFactory.GetUnitOfWork();
    }
    public async Task<IEnumerable<Product>> GetMostLikedProductsAsync()
    {
        var repository = this.repositoryFactory.ProductRepository;
        return await repository.GetMustLikedProducts();
    }
}

