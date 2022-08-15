using System;
namespace Meli.Proxies.Interfaces
{
    public interface ICouponClient
    {
        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>Returns Http Response message.</returns>
        Task<HttpResponseMessage> PostAsync(string path, object payload);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns Http Response message.</returns>
        Task<HttpResponseMessage> GetAsync(string path);
    }
}

