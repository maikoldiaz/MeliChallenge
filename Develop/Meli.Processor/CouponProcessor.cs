﻿using System.Collections.Concurrent;
using Meli.Entities;
using Meli.Processor.Interfaces;
using Meli.Proxies.Interfaces;

namespace Meli.Processor;
public class CouponProcessor : ICouponProcessor
{
    private readonly ICouponProxy couponProxy;
    public CouponProcessor(ICouponProxy _couponProxy)
    {
        this.couponProxy = _couponProxy;
    }

    public Task CreateItemAsync(List<string> items)
    {
        throw new NotImplementedException();
    }

    public async Task<CouponResponse> GetCouponAsync(Coupon coupon)
    {
        if (coupon == null) throw new ArgumentNullException("The content cannot be null and void", nameof(coupon));
        var exceptions = new ConcurrentQueue<Exception>();
        List<Product> products = new List<Product>();
        var tasks = coupon.ItemIds!.DistinctBy(dp => dp).AsParallel().Select(x => Task.Run(async () =>
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
        await Task.WhenAll(tasks);
        if (!exceptions.IsEmpty)
        {
            throw new AggregateException(exceptions);
        }
        return await this.CalculateCouponAsync(products, coupon.Amount);
    }

    private async Task<CouponResponse> CalculateCouponAsync(IEnumerable<Product> products, double couponPrice)
    {
        return await Task.Run<CouponResponse>(() => {
            var filteredList = products.Where(x => x.Id != null).OrderBy(p => p.Price).ToList();
            double maxPriceForCoupon = 0;
            List<Product> items = new List<Product>();
            for (int i = 0; i < products.Count(); i++)
            {
                maxPriceForCoupon += filteredList[i].Price;
                if (maxPriceForCoupon >= couponPrice)
                {
                    break;
                }
                items.Add(filteredList[i]);
            }
            return new CouponResponse { ItemIds = items.Select(x => x.Id!).ToList(), Total = items.Sum(x => x.Price) };
        });
    }
}
