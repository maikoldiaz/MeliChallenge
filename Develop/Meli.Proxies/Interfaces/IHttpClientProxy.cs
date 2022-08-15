using System;
namespace Meli.Proxies.Interfaces
{
    public interface IHttpClientProxy
    {
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="httpContent">Content of the HTTP.</param>
        /// <returns>The http response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpMethod method, Uri uri, HttpContent httpContent);
    }
}

