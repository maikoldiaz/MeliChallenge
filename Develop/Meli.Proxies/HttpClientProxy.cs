namespace Meli.Proxies;

using System;
using System.Threading.Tasks;
using Meli.Proxies.Interfaces;
using System.Net.Http;

public class HttpClientProxy : IHttpClientProxy
{
    private readonly IHttpClientFactory httpClient;
    public HttpClientProxy(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory;
    }

    public async Task<HttpResponseMessage> SendAsync(HttpMethod method, Uri uri, HttpContent httpContent)
    {
        var httpClient = this.httpClient.CreateClient();
        using var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.Headers.Accept.Clear();
            requestMessage.Content = httpContent;
            return await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
    }
}