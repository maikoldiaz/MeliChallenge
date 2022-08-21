namespace Meli.Proxies;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Meli.Proxies.Interfaces;
using Newtonsoft.Json;

public class CouponClient : ICouponClient
{
    private readonly IHttpClientProxy httpClientProxy;
    public CouponClient(IHttpClientProxy httpClientFactory)
    {
        this.httpClientProxy = httpClientFactory;
    }
    public async Task<HttpResponseMessage> PostAsync(string path, object payload)
    {
        using var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await this.httpClientProxy.SendAsync(
                        HttpMethod.Post,
                        this.BuildServiceUri(path),
                        content!);
        return response;
    }

    public async Task<HttpResponseMessage> GetAsync(string path)
    {
        var response = await this.httpClientProxy.SendAsync(
                    HttpMethod.Get,
                    this.BuildServiceUri(path),
                    null);
        return response;
    }

    private Uri BuildServiceUri(string path)
    {
        return new Uri($"https://api.mercadolibre.com/{path}");
    }
}
