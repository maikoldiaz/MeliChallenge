namespace Meli.Processor.Interfaces;
using System.Net.Http;
using Meli.Entities;
public interface ICouponProcessor
{
    Task<CouponResponse> GetCouponAsync(Coupon coupon);
    Task CreateItemAsync(List<Product> items);
}
