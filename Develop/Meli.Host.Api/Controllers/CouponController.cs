
namespace Meli.Host.Api.Controllers;

using Meli.Entities;
using Microsoft.AspNetCore.Mvc;
using Meli.Processor;
using Meli.Processor.Interfaces;

[ApiController]
[Route("[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponProcessor processor;

    public CouponController(ICouponProcessor couponProcessor)
    {
        processor = couponProcessor;
    }

    [HttpPost]
    public async Task<CouponResponse> GetItemsFromCouponAsync([FromBody] Coupon coupon)
    {
        return await processor.GetCouponAsync(coupon);
    }
}
