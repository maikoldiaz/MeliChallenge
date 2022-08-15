using System;
using Meli.Entities;
using Meli.Proxies.Interfaces;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Meli.Proxies;
public class CouponProxy : ICouponProxy
{
    private readonly ICouponClient couponClient;
    public CouponProxy(ICouponClient _couponClient)
    {
        couponClient = _couponClient;
    }

    public async Task<Product> ObtainProductAsync(string item)
    {
        var result = await this.couponClient.GetAsync($"items/{item}");
        return await result.Content.DeserializeHttpContentAsync<Product>();
    }
}


