using Meli.Entities;
using Meli.Processor.Interfaces;

namespace Meli.Processor;
public class CouponProcessor : ICouponProcessor
{
    public Task CreateItemAsync(List<string> items)
    {
        throw new NotImplementedException();
    }

    public async Task<CouponResponse> GetCouponAsync(Coupon coupon)
    {
        return await Task.Run<CouponResponse>(() =>
        {
            return new CouponResponse
            {
                ItemIds = new List<string> { "MELI1", "MELI2" },
                Total = 200
            };
        });
    }
}
