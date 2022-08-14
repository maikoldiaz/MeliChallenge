namespace Meli.Processor.Interfaces;
using System;
using Meli.Entities;
public interface ICouponProcessor
{
    Task<CouponResponse> GetCouponAsync(Coupon coupon);
    Task CreateItemAsync(List<string> items);
}
