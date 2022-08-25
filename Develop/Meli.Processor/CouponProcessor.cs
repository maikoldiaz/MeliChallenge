using System.Collections.Concurrent;
using Meli.Entities;
using Meli.Processor.Interfaces;
using Meli.Proxies.Interfaces;
using Meli.DataAccess.Interfaces;
using Meli.Core;

namespace Meli.Processor;
public class CouponProcessor : ICouponProcessor
{
    private readonly ICouponProxy couponProxy;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IUnitOfWork unitOfWork;
    private readonly IProductProcessor productProcessor;
    public CouponProcessor(ICouponProxy _couponProxy, IUnitOfWorkFactory _unitOfWorkFactory, IProductProcessor _productProcessor)
    {
        this.couponProxy = _couponProxy;
        this.productProcessor = _productProcessor;
        this.unitOfWorkFactory = _unitOfWorkFactory;
        this.unitOfWork = unitOfWorkFactory.GetUnitOfWork();
    }

    public async Task CreateItemAsync(List<Product> products)
    {
        ArgumentValidators.ThrowIfNull(products, nameof(products));
        await this.productProcessor.CreatePruductAsync(products);
    }

    public async Task<CouponResponse> GetCouponAsync(Coupon coupon)
    {
        ArgumentValidators.ThrowIfNull(coupon, nameof(coupon));
        var products = await this.GetProductsFromCouponAsync(coupon.ItemIds);
        return await this.CalculateCouponAsync(products, coupon.Amount);
    }

    public async Task<CouponResponse> CalculateCouponAsync(IEnumerable<Product> products, decimal couponPrice)
    {
        ArgumentValidators.ThrowIfNull(products, nameof(products));
        ArgumentValidators.ThrowIfNull(couponPrice, nameof(couponPrice));
        var filteredList = products.Where(x => x.Id != null).OrderBy(p => p.Price).ToList();
        var isSolve = false;
        List<Product> items = new();
        while (filteredList.Count != 0 && !isSolve)
        {
            var itemToAdd = filteredList.FirstOrDefault();
            filteredList.Remove(itemToAdd!);
            if (items.Sum(x => x.Price) + itemToAdd!.Price <= couponPrice)
            {
                items.Add(itemToAdd);
            }
            else
            {
                isSolve = true;
            }
        }
        await this.CreateItemAsync(items);
        return new CouponResponse { ItemIds = items.Select(x => x.Id!).ToList(), Total = items.Sum(x => x.Price) };
    }

    public async Task<IEnumerable<Product>> GetProductsFromCouponAsync(List<string> itemIds)
    {
        var exceptions = new ConcurrentQueue<Exception>();
        List<Product> products = new();
        var tasks = itemIds!.DistinctBy(dp => dp).AsParallel().Select(x => Task.Run(async () =>
        {
            try
            {
                var product = await couponProxy.ObtainProductAsync(x);
                products.Add(product);
            }
            catch (Exception e)
            {
                exceptions.Enqueue(e);
            }
        }));
        if (!exceptions.IsEmpty)
        {
            throw new AggregateException(exceptions);
        }
        await Task.WhenAll(tasks);
        return products;
    }
}
