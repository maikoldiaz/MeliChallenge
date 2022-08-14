using System;
using Meli.Entities;

namespace Meli.DataAccess.Interfaces;

public interface ICoupon : IRepository<Coupon>
{
    Task<CouponResponse> GetItemsFromCoupon(Coupon coupon);
}

