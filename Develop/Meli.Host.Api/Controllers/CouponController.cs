namespace Meli.Host.Api.Controllers;

using Meli.Entities;
using Microsoft.AspNetCore.Mvc;
using Meli.Processor.Interfaces;

[ApiController]
[Route("[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponProcessor processor;
    private readonly IHttpClientFactory httpClient;

    public CouponController(ICouponProcessor couponProcessor, IHttpClientFactory httpClientFactory)
    {
        processor = couponProcessor;
        httpClient = httpClientFactory;
    }

    [HttpPost]
    public async Task<CouponResponse> GetItemsFromCouponAsync([FromBody] Coupon coupon)
    {
        return await processor.GetCouponAsync(coupon);
    }
}
