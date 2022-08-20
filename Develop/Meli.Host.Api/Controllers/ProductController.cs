namespace Meli.Host.Api.Controllers;

using Meli.Entities;
using Microsoft.AspNetCore.Mvc;
using Meli.Processor.Interfaces;
using Meli.DataAccess.Interfaces;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductProcessor processor;
    private readonly IProductRepository productRepository;

    public ProductController(IProductProcessor productProcessor, IProductRepository productRepository)
    {
        processor = productProcessor;
        this.productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetItemsFromCouponAsync()
    {
        return await processor.GetMostLikedProductsAsync();
    }
}